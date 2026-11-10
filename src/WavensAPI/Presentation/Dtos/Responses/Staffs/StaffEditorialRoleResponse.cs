namespace WavensApi.Presentation.Dtos.Responses.Staffs
{
    public sealed record StaffEditorialRoleResponse
    {
        public required Guid Id { get; init; } 
        public required string Name { get; init; }
    }
}
