using Microsoft.EntityFrameworkCore;
using WavensApi.Domain.Entities.Staffs;

namespace WavensApi.Infrastructure.Persistence.Data.Seeds
{
    public class EditorialRoleSeeder(WavensDbContext context)
    {
        private readonly WavensDbContext _context = context;

        public async Task SeedAsync()
        {
            var editorialRoles = new[]
            {
                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "ADMIN",
                    Description = "System administrator."
                },

                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "MANAGER",
                    Description = "Management and analytics access."
                },

                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "EDITOR_IN_CHIEF",
                    Description = "Editorial leadership and publishing authority."
                },

                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "EDITOR",
                    Description = "Editorial review and approval."
                },

                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "REPORTER",
                    Description = "Article creation and submission."
                },

                new EditorialRole
                {
                    Id = Guid.NewGuid(),
                    Name = "PHOTOGRAPHER",
                    Description = "Media upload and management."
                }
            };

            var existingRoles = await _context.EditorialRoles.Select(role => role.Name).ToListAsync();

            var missingRoles = editorialRoles
                .Where(editorialRoles => !existingRoles.Contains(editorialRoles.Name))
                .ToList();

            if (missingRoles.Count == 0)
            {
                return;
            }

            _context.EditorialRoles.AddRange(missingRoles);

            await _context.SaveChangesAsync();
        }
    }
}
