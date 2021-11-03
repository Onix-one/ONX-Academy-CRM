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
        private readonly UserManager<User> _userManager;
        private ClaimsPrincipal _user;
        public ViewBagActionFilter(IManagerService managerService, IStudentService studentService, UserManager<User> userManager)
        {
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
                    if (manager.Image != null && manager.Image.Length != 0)
                    {
                        return manager.Image;
                    }
                }
            }
            return new byte[] { };
        }
    }
}
