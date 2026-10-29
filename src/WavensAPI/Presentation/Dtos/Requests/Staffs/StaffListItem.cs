using WavensApi.Domain.Enums.Staffs;

namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record StaffListItem
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required IReadOnlyCollection<string> Roles { get; init; }
        public StaffStatus Status { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
