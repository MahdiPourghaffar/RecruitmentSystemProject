using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Seeders
{
    public class Seed
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<DataContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            if (!context.Roles.Any()) await RolesSeeder.SeedRolesAsync(roleManager);
            if (!context.Users.Any(x => x.UserName == "Admin"))await UsersSeeder.SeedUsersAsync(userManager);
        }
    }
}
