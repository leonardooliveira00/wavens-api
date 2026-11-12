using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WavensApi.Domain.Entities.Staffs;

namespace WavensApi.Infrastructure.Persistence.Data.Configurations.Staffs
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");

            builder.HasKey(staff => staff.Id);

            builder.HasIndex(staff => new
            {
                staff.CreatedAt,
                staff.Id
            });

            builder.Property(staff => staff.FirstName).HasMaxLength(50).IsRequired();

            builder.Property(staff => staff.LastName).HasMaxLength(50).IsRequired();

            builder.Property(staff => staff.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(staff => staff.Email).IsUnique();

            builder.Property(staff => staff.ContactPhone).HasMaxLength(20).IsRequired();

            builder.Property(staff => staff.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

            builder.OwnsOne(staff => staff.Address, address =>
            {
                address.ToTable("Addresses");

                address
                .WithOwner()
                .HasForeignKey(staff => staff.StaffId);

                address.HasKey(staff => staff.StaffId);

                address.Property(address => address.Street).HasMaxLength(100).IsRequired();

                address.Property(address => address.Number).HasMaxLength(10).IsRequired();

                address.Property(address => address.Complement).HasMaxLength(100);

                address.Property(address => address.District).HasMaxLength(50).IsRequired();

                address.Property(address => address.City).HasMaxLength(50).IsRequired();

                address.Property(address => address.State).HasMaxLength(50).IsRequired();

                address.Property(address => address.Cep).HasMaxLength(8).IsRequired();
            });
        }
    }
}
