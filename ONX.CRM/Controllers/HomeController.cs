using Microsoft.AspNetCore.Mvc;

namespace ONX.CRM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("RequestsForCourses", "Requests");

            }

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
