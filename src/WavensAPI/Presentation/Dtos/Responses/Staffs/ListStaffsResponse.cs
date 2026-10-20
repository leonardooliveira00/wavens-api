using WavensApi.Domain.Entities.Authorization;
using WavensApi.Domain.Enums.Staffs;

namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record ListStaffsResponse
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string FullName { get; init; }
        public required string Email { get; init; }
        public StaffStatus Status { get; init; }
    }
}
