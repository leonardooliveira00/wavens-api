using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WavensApi.Application.Common;
using WavensApi.Application.Common.Application;
using WavensApi.Application.Common.Errors;
using WavensApi.Application.Common.Pagination;
using WavensApi.Domain.Entities.Identity.Users;
using WavensApi.Domain.Entities.Staffs;
using WavensApi.Domain.Enums.Identity.Users;
using WavensApi.Domain.Enums.Staffs;
using WavensApi.Infrastructure.Persistence;
using WavensApi.Presentation.Dtos.Requests.Staffs;
using Npgsql;

namespace WavensApi.Application.Staffs
{
    public class StaffService(
        WavensDbContext wavensDbContext,
        UserManager<User> userManager)
    {
        private readonly WavensDbContext _context = wavensDbContext;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<ApplicationResult<Staff>> RegisterStaffAsync(RegisterStaffRequest request)
        {
            var staffExists = await _context.Staffs.AnyAsync(staff => staff.Email == request.Email);

            if (staffExists) return ApplicationResult<Staff>.Failure(StaffErrors.StaffAlreadyExists);

            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email,
                ContactPhone = request.ContactPhone.Trim(),
            };

            _context.Staffs.Add(staff);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (
                    ex.InnerException is PostgresException pgEx &&
                    pgEx.SqlState == PostgresErrorCodes.UniqueViolation &&
                    pgEx.ConstraintName == "IX_Staffs_Email")
            {
                return ApplicationResult<Staff>.Failure(StaffErrors.EmailAlreadyInUse);
            }

            return ApplicationResult<Staff>.Success(staff);
        }

        private static string FormatRoleName(string roleName)
        {
            var formatted = roleName.Replace('_', '-').ToLowerInvariant();

            return char.ToUpperInvariant(formatted[0]) + formatted[1..];
        }

        public async Task<ApplicationResult<KeysetPagedResult<StaffListItem>>> ListAllStaffsAsync(
            StaffStatus? status,
            StaffSortBy? sortBy,
            SortOrder? sortOrder,
            string? search,
            StaffKeysetCursor? currentCursor,
            int pageSize
            )
        {
            if (pageSize <= 0 || pageSize > 100) return ApplicationResult<KeysetPagedResult<StaffListItem>>.Failure(KeysetPaginationErrors.InvalidPageSize);

            sortBy ??= StaffSortBy.CreatedAt;
            sortOrder ??= SortOrder.Desc;

            if (!Enum.IsDefined(sortBy.Value)) throw new BadHttpRequestException($"Invalid sortBy value: {sortBy.Value}");

            var query = _context.Staffs
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue) query = query.Where(staff => staff.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = $"%{search.Trim()}%";

                query = query.Where(staff =>
                EF.Functions.ILike(staff.FirstName + " " + staff.LastName, normalizedSearch) ||
                EF.Functions.ILike(staff.Email, normalizedSearch)
                );
            }

            if (currentCursor is not null)
            {

                if (currentCursor is not CreatedAtCursor createdAtCursor)
                {
                    throw new BadHttpRequestException("Invalid cursor type.");
                }

                if (currentCursor is not FirstNameCursor firstNameCursor)
                {
                    throw new BadHttpRequestException("Invalid cursor type.");
                }

                if (currentCursor is not LastNameCursor lastNameCursor)
                {
                    throw new BadHttpRequestException("Invalid cursor type.");
                }

                query = sortBy switch
                {
                    StaffSortBy.CreatedAt => sortOrder == SortOrder.Desc
                    ? query.Where(staff => staff.CreatedAt < createdAtCursor.CreatedAt || (staff.CreatedAt == createdAtCursor.CreatedAt && staff.Id < createdAtCursor.Id))
                    : query.Where(staff => staff.CreatedAt > createdAtCursor.CreatedAt || (staff.CreatedAt == createdAtCursor.CreatedAt && staff.Id > createdAtCursor.Id)),

                    StaffSortBy.FirstName => sortOrder == SortOrder.Desc
                    ? query.Where(staff => string.Compare(staff.FirstName, firstNameCursor.FirstName) < 0 || (string.Compare(staff.FirstName, firstNameCursor.FirstName) == 0 && staff.Id < firstNameCursor.Id))
                    : query.Where(staff => string.Compare(staff.FirstName, firstNameCursor.FirstName) > 0 || (string.Compare(staff.FirstName, firstNameCursor.FirstName) == 0 && staff.Id > firstNameCursor.Id)),

                    StaffSortBy.LastName => sortOrder == SortOrder.Desc
                    ? query.Where(staff => string.Compare(staff.LastName, lastNameCursor.LastName) < 0 || (string.Compare(staff.LastName, lastNameCursor.LastName) == 0 && staff.Id < lastNameCursor.Id))
                    : query.Where(staff => string.Compare(staff.LastName, lastNameCursor.LastName) > 0 || (string.Compare(staff.LastName, lastNameCursor.LastName) == 0 && staff.Id > lastNameCursor.Id)),

                    _ => throw new BadHttpRequestException($"Invalid sortBy value: {sortBy.Value}")
                };
            }

            query = sortBy switch
            {
                StaffSortBy.CreatedAt => sortOrder == SortOrder.Desc
                ? query.OrderByDescending(staff => staff.CreatedAt).ThenByDescending(staff => staff.Id)
                : query.OrderBy(staff => staff.CreatedAt).ThenBy(staff => staff.Id),

                StaffSortBy.FirstName => sortOrder == SortOrder.Desc
                ? query.OrderByDescending(staff => staff.FirstName).ThenByDescending(staff => staff.Id)
                : query.OrderBy(staff => staff.FirstName).ThenBy(staff => staff.Id),

                StaffSortBy.LastName => sortOrder == SortOrder.Desc
                ? query.OrderByDescending(staff => staff.LastName).ThenByDescending(staff => staff.Id)
                : query.OrderBy(staff => staff.LastName).ThenBy(staff => staff.Id),

                _ => throw new BadHttpRequestException($"Invalid sortBy value: {sortBy.Value}")
            };

            var staffs = await query
                .Take(pageSize + 1)
                .Select(staff => new StaffListItem
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Email = staff.Email,
                    Status = staff.Status,
                    Roles = _context.UserRoles.Where(
                        userRole => userRole.User.StaffId == staff.Id)
                    .Select(userRole => userRole.Role.Name)
                    .ToList(),
                    CreatedAt = staff.CreatedAt,
                })
                .ToListAsync();

            staffs = [.. staffs.Select(staff => staff with
            {
                Roles = [.. staff.Roles.Select(FormatRoleName)]
            })];

            var hasMore = staffs.Count > pageSize;

            if (hasMore) staffs.RemoveAt(staffs.Count - 1);

            var lastStaff = staffs.LastOrDefault();

            string? nextCursor = null;

            if (hasMore && lastStaff is not null)
            {
                StaffKeysetCursor nextCursorData = sortBy switch
                {
                    StaffSortBy.CreatedAt => new CreatedAtCursor(
                        CreatedAt: lastStaff.CreatedAt,
                        Id: lastStaff.Id,
                        SortOrder: sortOrder.Value
                    ),

                    StaffSortBy.FirstName => new FirstNameCursor(
                        FirstName: lastStaff.FirstName,
                        Id: lastStaff.Id,
                        SortOrder: sortOrder.Value
                    ),

                    StaffSortBy.LastName => new LastNameCursor(
                        LastName: lastStaff.LastName,
                        Id: lastStaff.Id,
                        SortOrder: sortOrder.Value
                        ),

                    _ => throw new BadHttpRequestException($"Invalid sortBy cursor value: {sortBy.Value}")
                };

                nextCursor = KeysetCursorCodec.Encode(nextCursorData, typeof(StaffKeysetCursor));
            }

            var pagedResult = new KeysetPagedResult<StaffListItem>
            {
                Items = staffs,
                HasMore = hasMore,
                NextCursor = nextCursor
            };

            return ApplicationResult<KeysetPagedResult<StaffListItem>>.Success(pagedResult);
        }

        public async Task<ApplicationResult<Staff>> FindStaffByIdAsync(Guid staffId)
        {
            var staff = await _context.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(staff => staff.Id == staffId);

            if (staff is null) return ApplicationResult<Staff>.Failure(StaffErrors.NotFound);

            return ApplicationResult<Staff>.Success(staff);
        }

        public async Task<ApplicationResult<Staff>> UpdateStaffAsync(UpdateStaffRequest request, Guid staffId)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(staff => staff.Id == staffId);

            if (staff is null) return ApplicationResult<Staff>.Failure(StaffErrors.NotFound);

            if (staff.Status != StaffStatus.Active) return ApplicationResult<Staff>.Failure(StaffErrors.NotActive);

            UpdateHelper.UpdateIfProvided(
                request.FirstName,
                value => staff.FirstName = value.Trim()
                );

            UpdateHelper.UpdateIfProvided(
                request.LastName,
                value => staff.LastName = value.Trim()
                );

            UpdateHelper.UpdateIfProvided(
                request.ContactPhone,
                value => staff.ContactPhone = value.Trim()
                );

            if (request.Email is not null)
            {

                if (request.Email != staff.Email)
                {
                    var emailAlreadyInUse = await _context.Staffs
                    .AnyAsync(otherStaff => otherStaff.Id != staffId &&
                              otherStaff.Email == request.Email
                    );

                    if (emailAlreadyInUse) return ApplicationResult<Staff>.Failure(StaffErrors.EmailAlreadyInUse);

                    staff.Email = request.Email;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException postgresException &&
                    postgresException.SqlState == PostgresErrorCodes.UniqueViolation &&
                    postgresException.ConstraintName == "IX_Staffs_Email"
                )
            {
                return ApplicationResult<Staff>.Failure(StaffErrors.EmailAlreadyInUse);
            }

            return ApplicationResult<Staff>.Success(staff);
        }

        public async Task<ApplicationResult> DeactivateStaffAsync(Guid staffId)
        {
            var staff = await _context.Staffs
                .Include(staff => staff.User)
                .FirstOrDefaultAsync(staff => staff.Id == staffId);

            if (staff is null) return ApplicationResult.Failure(StaffErrors.NotFound);

            if (staff.Status != StaffStatus.Active) return ApplicationResult.Failure(StaffErrors.NotActive);

            await using var transaction = await _context.Database.BeginTransactionAsync();

            staff.Status = StaffStatus.Inactive;

            if (staff.User is not null)
            {
                staff.User.Status = UserStatus.Inactive;

                var stampResult = await _userManager.UpdateSecurityStampAsync(staff.User);

                if (!stampResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Failed to update the user's security stamp."
                    );
                }
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return ApplicationResult.Success();
        }
    }
}
