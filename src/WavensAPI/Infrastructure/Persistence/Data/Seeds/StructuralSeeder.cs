namespace WavensApi.Infrastructure.Persistence.Data.Seeds
{
    public class StructuralSeeder(
        EditorialRoleSeeder editorialRoleSeeder,
        RoleSeeder roleSeeder, 
        PermissionSeeder permissionSeeder, 
        RolePermissionSeeder rolePermissionSeeder
        )
    {
        private readonly RoleSeeder _roleSeeder = roleSeeder;
        private readonly PermissionSeeder _permissionSeeder = permissionSeeder;
        private readonly RolePermissionSeeder _rolePermissionSeeder = rolePermissionSeeder;
        private readonly EditorialRoleSeeder _editorialRoleSeeder = editorialRoleSeeder;

        public async Task SeedAsync()
        {
            await _editorialRoleSeeder.SeedAsync();
            await _roleSeeder.SeedAsync();
            await _permissionSeeder.SeedAsync();
            await _rolePermissionSeeder.SeedAsync();
        }
    }
}
