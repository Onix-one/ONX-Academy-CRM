using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class UserService : IUserService
    {
        private static INotificationService _notificationService;
        private readonly UserManager<User> _userManager;
        public UserService(INotificationService notificationService, UserManager<User> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }
        public async Task CreateAsync(User user, string role)
        {
            var passwordLength = 10;
            var password = CreateRandomPassword(passwordLength);
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            await _notificationService.SendEmailAsync(user.Email,
                user.FirstName, user.LastName, password,
                "Access to the system ONX Academy");
        }
        public async Task Update(string id, string email, string firstName, string lastName)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                user.UserName = email;
                user.LastName = lastName;
                user.FirstName = firstName;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("User not created!");
                }
            }
        }
        public async Task Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }
        private string CreateRandomPassword(int passwordLength)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[passwordLength];
            var allowedCharCount = allowedChars.Length;

            for (var i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[(int)((allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
