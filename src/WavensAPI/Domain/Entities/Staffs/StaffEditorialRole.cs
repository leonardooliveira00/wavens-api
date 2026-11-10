namespace WavensApi.Domain.Entities.Staffs
{
    public class StaffEditorialRole
    {
        public Guid StaffId { get; set; }
        public Guid EditorialRoleId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public required Staff Staff { get; set; }
        public required EditorialRole EditorialRole { get; set; }
    }
}
