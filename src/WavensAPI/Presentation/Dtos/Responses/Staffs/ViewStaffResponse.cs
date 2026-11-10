using WavensApi.Domain.Enums.Staffs;

namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record ViewStaffResponse
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly BirthDate { get; init; }
        public required string Email { get; init; }
        public required string ContactPhone { get; init; }  
        public required AddressResponse Address { get; init; }
        public required IReadOnlyCollection<StaffEditorialRoleResponse> EditorialRoles { get; init; }
        public StaffStatus Status { get; init; }
    }
}
