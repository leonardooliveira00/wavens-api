namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record RegisterAddressResponse
    {
        public required string Street { get; init; }
        public required string Number { get; init; }
        public string? Complement { get; init; }
        public required string District { get; init; }
        public required string State { get; init; }
        public required string City { get; init; }
        public required string Cep { get; init; }
    }
}
