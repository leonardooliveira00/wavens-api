namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record UpdateAddressRequest
    {
        public string? Street { get; init; }
        public string? Number { get; init; }
        public string? Complement { get; init; }
        public string? District { get; init; }
        public string? State { get; init; }
        public string? City { get; init; }
        public string? Cep { get; init; }
    }
}
