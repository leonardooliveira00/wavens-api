using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WavensApi.Domain.Entities.Staffs;

namespace WavensApi.Infrastructure.Persistence.Data.Configurations.Staffs
{
    public class StaffEditorialRoleConfiguration : IEntityTypeConfiguration<StaffEditorialRole>
    {
        public void Configure(EntityTypeBuilder<StaffEditorialRole> builder)
        {
            builder.ToTable("StaffEditorialRoles");

            builder.HasKey(staffRole => new
            {
                staffRole.StaffId,
                staffRole.EditorialRoleId
            });

            builder.Property(staffRole => staffRole.AssignedAt).IsRequired();

            builder
                .HasOne(staffRole => staffRole.Staff)
                .WithMany(staff => staff.StaffEditorialRoles)
                .HasForeignKey(staffRole => staffRole.StaffId);

            builder
                .HasOne(staffRole => staffRole.EditorialRole)
                .WithMany(role => role.StaffEditorialRoles)
                .HasForeignKey(staffRole => staffRole.EditorialRoleId);
        }
    }
}
