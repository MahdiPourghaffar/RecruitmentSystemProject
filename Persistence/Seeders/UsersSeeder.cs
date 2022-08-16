using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeders
{
    public static class UsersSeeder
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            var admin = new User
            {

                UserName = "Admin",
                Email = "admin@test.ir",
                PhoneNumber = "09300000000",
                Name = "Admin",
                Location = "Tabriz, Iran",

            };
            await userManager.CreateAsync(admin, "@Admin123");
            await userManager.AddToRoleAsync(admin, nameof(Roles.RolesEnum.Admin));

        }
    }
}
