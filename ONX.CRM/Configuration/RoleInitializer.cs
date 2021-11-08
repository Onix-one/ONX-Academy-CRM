using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.Configuration
{
    public static class RoleInitializer
    {
        private static UserManager<User> _userManager;
        private static RoleManager<IdentityRole> _roleManager;

        public static async Task InitializeAsync(IServiceProvider serviceProvider, IOptions<SecurityOptions> securityOptions,
            IManagerService managerService, ITeacherService teacherService, IStudentService studentService)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = securityOptions.Value.AdminEmail;
            string adminPassword = securityOptions.Value.AdminPassword;
            
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
                User admin = new User { LastName = "Smith", FirstName = "Alex", Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                }
            }

            string managerEmail = securityOptions.Value.ManagerEmail;
            string managerPassword = securityOptions.Value.ManagerPassword;

            if (await _userManager.FindByNameAsync(managerEmail) == null)
            {
                User user = new User { LastName = "Smith", FirstName = "John", Email = managerEmail, UserName = managerEmail };
                IdentityResult result = await _userManager.CreateAsync(user, managerPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "manager");
                }

                var manager = await managerService.GetEntityByIdAsync(1);
                manager.UserId = user.Id;
                managerService.Update(manager);
            }

            string studentEmail = securityOptions.Value.StudentEmail;
            string studentPassword = securityOptions.Value.StudentPassword;

            if (await _userManager.FindByNameAsync(studentEmail) == null)
            {
                User user = new User { LastName = "Lazarev", FirstName = "Alex", Email = studentEmail, UserName = studentEmail };
                IdentityResult result = await _userManager.CreateAsync(user, studentPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "student");
                }
                var student = await studentService.GetEntityByIdAsync(1);
                student.UserId = user.Id;
                studentService.Update(student);
            }

            string teacherEmail = securityOptions.Value.TeacherEmail;
            string teacherPassword = securityOptions.Value.TeacherPassword;

            if (await _userManager.FindByNameAsync(teacherEmail) == null)
            {
                User user = new User { LastName = "Папко", FirstName = "Вадим", Email = teacherEmail, UserName = teacherEmail };
                IdentityResult result = await _userManager.CreateAsync(user, teacherPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "teacher");
                }
                var teacher = await teacherService.GetEntityByIdAsync(1);
                teacher.UserId = user.Id;
                teacherService.Update(teacher);
            }
        }
    }
}
