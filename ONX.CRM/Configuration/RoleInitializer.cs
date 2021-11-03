using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.Configuration
{
    public static class RoleInitializer
    {
        private static UserManager<User> _userManager;
        private static RoleManager<IdentityRole> _roleManager;

        public static async Task InitializeAsync(IServiceProvider serviceProvider, IOptions<SecurityOptions> securityOptions)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = securityOptions.Value.AdminEmail;
            string password = securityOptions.Value.AdminPassword;

            var roles = new List<string>() { "admin", "manager", "student", "teacher" };

            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User {LastName = "Smith", FirstName = "Alex", Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await _userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
