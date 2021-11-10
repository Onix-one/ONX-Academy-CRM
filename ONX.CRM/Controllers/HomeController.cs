using Microsoft.AspNetCore.Mvc;
using ONX.CRM.Filters;

namespace ONX.CRM.Controllers
{
    public class HomeController : BaseController
    {
        [TypeFilter(typeof(LocalExceptionFilter))]
        public IActionResult Index()
        {
            if (User.IsInRole("teacher"))
            {
                return RedirectToAction("Home", "Teachers");
            }
            if (User.IsInRole("manager"))
            {
                return RedirectToAction("Index", "Requests");
            }
            if (User.IsInRole("student"))
            {
                return RedirectToAction("Home", "Students");
            }
            if (User.IsInRole("admin"))
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
