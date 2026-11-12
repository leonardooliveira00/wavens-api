namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record RegisterStaffResponse
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly BirthDate { get; init; }
        public required string Email { get; init; }
        public required string ContactPhone { get; init; }
        public required RegisterAddressResponse Address { get; init; }
        public required IReadOnlyCollection<StaffEditorialRoleResponse> EditorialRoles { get; init; }
    }
}
