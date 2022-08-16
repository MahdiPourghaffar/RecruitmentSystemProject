using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeders
{
    public class RolesSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            var adminRole = new Role
            {
                Name = "Admin",
                Description= "Full Access",
            };
            var companyRole = new Role
            {
                Name = "Company",
                Description = "CRUD to Announcement",
            };
            var userRole = new Role
            {
                Name = "User",
                Description = "No Access",
            };
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(companyRole);
            await roleManager.CreateAsync(userRole);
        }
    }
}
