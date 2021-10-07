using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ONX.CRM.BLL.Models;

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

            string managerEmail = securityOptions.Value.ManagerEmail;
            string password = securityOptions.Value.ManagerPassword;

            if (await _roleManager.FindByNameAsync("manager") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if (await _roleManager.FindByNameAsync("student") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("student"));
            }
            if (await _userManager.FindByNameAsync(managerEmail) == null)
            {
                User admin = new User { Email = managerEmail, UserName = managerEmail };
                IdentityResult result = await _userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "manager");
                }
            }
        }
    }
}
