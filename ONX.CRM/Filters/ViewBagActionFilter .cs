using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.Controllers;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.Filters
{
    public class ViewBagActionFilter : ActionFilterAttribute
    {
        private readonly IManagerService _managerService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;
        private ClaimsPrincipal _user;
        public ViewBagActionFilter(IManagerService managerService, IStudentService studentService,
            ITeacherService teacherService, UserManager<User> userManager)
        {
            _teacherService = teacherService;
            _managerService = managerService;
            _studentService = studentService;
            _userManager = userManager;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
           _user = context.HttpContext.User;
          
            if (context.Controller is Controller contextController)
            {
                contextController.ViewBag.Theme = BaseController.Theme ?? "dark-theme";
                BaseController.Photo = SetPhoto();
                contextController.ViewBag.Photo = BaseController.Photo ?? new byte[] { };
                if (_user.Identity != null) contextController.ViewBag.UserName = GetUserEmail();
            }
            base.OnResultExecuting(context);
        }
        private byte[] SetPhoto()
        {
            if (_user.Identity != null)
            {
                var userId = _userManager.GetUserId(_user);
                if (_user.IsInRole("admin"))
                {
                    return new byte[] { };
                }
                if (_user.IsInRole("manager"))
                {
                    var manager = _managerService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                    if (manager is {Image: { }} && manager.Image.Length != 0)
                    {
                        return manager.Image;
                    }
                }
                if (_user.IsInRole("student"))
                {
                    var student = _studentService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                    if (student is { Image: { } } && student.Image.Length != 0)
                    {
                        return student.Image;
                    }
                }
                if (_user.IsInRole("teacher"))
                {
                    var teacher = _teacherService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                    if (teacher is { Image: { } } && teacher.Image.Length != 0)
                    {
                        return teacher.Image;
                    }
                }
            }
            return new byte[] { };
        }
        private string GetUserEmail()
        {
            if (_user.Identity != null)
            {
                var userId = _userManager.GetUserId(_user);
                if (_user.IsInRole("admin"))
                {
                    return "Admin@mail.ru";
                }
                if (_user.IsInRole("manager"))
                {
                    var manager = _managerService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                    if (manager != null) return manager.Email;
                }
                if (_user.IsInRole("student"))
                {
                    var student = _studentService.FindByUserIdAsync(userId).Result.FirstOrDefault();
                    if (student != null) return student.Email;
                }
                if (_user.IsInRole("teacher"))
                {
                    var teacher = _teacherService.FindByUserIdAsync(userId).Result.FirstOrDefault();

                    if (teacher != null) return teacher.Email;
                }
            }
            return "";
        }
    }
}
