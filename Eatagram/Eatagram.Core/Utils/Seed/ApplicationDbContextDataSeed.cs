using Eatagram.Core.Configuration;
using Eatagram.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Utils.Seed
{
    public class ApplicationDbContextDataSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdenityConstants.Roles.Administrator));
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdenityConstants.Roles.Member));

            string adminUserName = "GabrieleToffanin@outlook.it";

            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminUserName,
                EmailConfirmed = true,
                FirstName = "Gabriele",
                LastName = "Admin"
            };

            await userManager.CreateAsync(adminUser, ApplicationIdenityConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, ApplicationIdenityConstants.Roles.Administrator);
        }
    }
}
