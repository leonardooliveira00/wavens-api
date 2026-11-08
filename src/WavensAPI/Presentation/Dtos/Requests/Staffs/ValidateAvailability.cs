namespace WavensApi.Presentation.Dtos.Requests.Staffs
{
    public sealed record ValidateAvailability
    {
        public string? Email { get; init; }
        public string? ContactPhone { get; init; }
    }
}
