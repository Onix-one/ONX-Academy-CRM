using Microsoft.AspNetCore.Mvc;
using ONX.CRM.Filters;

namespace ONX.CRM.Controllers
{
    public class HomeController : BaseController
    {
        [TypeFilter(typeof(LocalExceptionFilter))]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Requests");
            }
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
