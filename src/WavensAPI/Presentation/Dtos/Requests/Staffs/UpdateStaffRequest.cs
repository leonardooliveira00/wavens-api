namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record UpdateStaffRequest
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public DateOnly? BirthDate { get; init; }
        public string? Bio { get; init; }
        public string? Email { get; init; }
        public string? ContactPhone { get; init; }
        public UpdateAddressRequest? Address { get; init; }
    }
}
