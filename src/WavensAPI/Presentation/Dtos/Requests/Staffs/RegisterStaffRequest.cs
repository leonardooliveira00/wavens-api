namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record RegisterStaffRequest
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly BirthDate { get; init; }
        public required string Email { get; init; }
        public required string ContactPhone { get; init; }
        public required AddressRequest Address { get; init; }
        public required IReadOnlyCollection<Guid> EditorialRoleIds { get; init; }
    }
}