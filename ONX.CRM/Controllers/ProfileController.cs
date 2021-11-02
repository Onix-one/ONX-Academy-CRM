using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ONX.CRM.DAL.Models;
using ONX.CRM.Filters;

namespace ONX.CRM.Controllers
{
    [Authorize]
    [TypeFilter(typeof(LocalExceptionFilter))]
    public class ProfileController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {


            return View();
        }
    }
}
