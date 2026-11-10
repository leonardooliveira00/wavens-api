namespace WavensApi.Domain.Entities.Staffs
{
    public class EditorialRole
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public IReadOnlyCollection<StaffEditorialRole> StaffEditorialRoles { get; set; } = [];
    }
}
