using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WavensApi.Application.Staffs;
using WavensApi.Application.Common.Pagination;
using WavensApi.Domain.Constants.Authorization;
using WavensApi.Domain.Enums.Staffs;
using WavensApi.Presentation.Dtos.Requests.Staffs;
using WavensApi.Presentation.Dtos.Responses.Staffs;
using WavensApi.Presentation.Extensions.Http;

namespace WavensApi.Presentation.Controllers.Staffs
{
    [Route("api/staffs")]
    [ApiController]
    public class StaffsController(
        StaffService staffService
        ) : ControllerBase
    {
        private readonly StaffService _staffService = staffService;

        [Authorize(Policy = PermissionConstants.CreateStaff)]
        [HttpPost()]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffRequest request)
        {
            var result = await _staffService.RegisterStaffAsync(request);

            if (!result.Succeeded) return result.ToErrorResult();

            var staff = result.Data!;

            var response = new RegisterStaffResponse
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                ContactPhone = staff.ContactPhone,
                CreatedAt = staff.CreatedAt,
                UpdatedAt = staff.UpdatedAt,
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize(Policy = PermissionConstants.ViewStaff)]
        [HttpGet]
        public async Task<IActionResult> ListAllStaffs(
            [FromQuery] StaffStatus? status,
            [FromQuery] StaffSortBy? sortBy,
            [FromQuery] SortOrder? sortOrder,
            [FromQuery] string? search,
            [FromQuery] string? cursor,
            [FromQuery] int pageSize = 20
            )
        {
            StaffKeysetCursor? currentCursor = null;

            if (!string.IsNullOrWhiteSpace(cursor))
            {
                if (!KeysetCursorCodec.TryDecode<StaffKeysetCursor>(
                    cursor,
                    typeof(StaffKeysetCursor),
                    out currentCursor))
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }

            var result = await _staffService.ListAllStaffsAsync(
                status,
                sortBy,
                sortOrder,
                search,
                currentCursor,
                pageSize
                );

            if (!result.Succeeded) return result.ToErrorResult();

            var pagedResult = result.Data!;

            var items = pagedResult.Items
                .Select(staff => new ListStaffsResponse
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    FullName = $"{staff.FirstName} {staff.LastName}",
                    Email = staff.Email,
                    Status = staff.Status
                })
                .ToList();

            var response = new KeysetPagedResponse<ListStaffsResponse>
            {
                Items = items,
                HasMore = pagedResult.HasMore,
                NextCursor = pagedResult.NextCursor
            };

            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.ViewStaff)]
        [HttpGet("{staffId:guid}")]
        public async Task<IActionResult> ViewStaffById(Guid staffId)
        {
            var result = await _staffService.FindStaffByIdAsync(staffId);

            if (!result.Succeeded) return result.ToErrorResult();

            var staff = result.Data!;

            var response = new ViewStaffResponse
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                ContactPhone = staff.ContactPhone,
                Status = staff.Status
            };

            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.UpdateStaff)]
        [HttpPatch("{staffId:guid}")]
        public async Task<IActionResult> UpdateStaff([FromBody] UpdateStaffRequest request, Guid staffId)
        {
            var result = await _staffService.UpdateStaffAsync(request, staffId);

            if (!result.Succeeded) return result.ToErrorResult();

            var staff = result.Data!;

            var response = new UpdateStaffResponse
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                ContactPhone = staff.ContactPhone,
            };

            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.DeactivateStaff)]
        [HttpPatch("{staffId:guid}/deactivate")]
        public async Task<IActionResult> DeactivateStaff(Guid staffId)
        {
            var result = await _staffService.DeactivateStaffAsync(staffId);

            if (!result.Succeeded) return result.ToErrorResult();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
