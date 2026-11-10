using WavensApi.Domain.Common;
using WavensApi.Domain.Entities.Identity.Users;
using WavensApi.Domain.Enums.Staffs;

namespace WavensApi.Domain.Entities.Staffs
{
    public class Staff : AuditableEntity
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDate { get; set; }
        public string? Bio { get; set; }
        public required string Email { get; set; }
        public required string ContactPhone { get; set; }
        public required Address Address { get; set; }
        public IReadOnlyCollection<StaffEditorialRole> StaffEditorialRoles { get; set; } = [];
        public User? User { get; set; }
        public StaffStatus Status { get; set; } = StaffStatus.Active;
    }
}