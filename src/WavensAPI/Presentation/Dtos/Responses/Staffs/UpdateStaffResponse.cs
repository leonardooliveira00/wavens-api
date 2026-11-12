namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed class UpdateStaffResponse
    {
        public Guid Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required DateOnly BirthDate { get; init; }
        public string? Bio { get; init; }
        public required string Email { get; init; }
        public required string ContactPhone { get; init; }
        public required UpdateAddressResponse Address { get; init; }
    }
}
