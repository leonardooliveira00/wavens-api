namespace WavensApi.Presentation.Dtos.Responses.Authentication
{
    public sealed record CurrentUserResponse
    {
        public Guid Id { get; init; }
        public required string Email { get; init; }

        public required IReadOnlyCollection<string> Roles { get; init; }

        public required IReadOnlyCollection<string> Permissions { get; init; }
    }
}
