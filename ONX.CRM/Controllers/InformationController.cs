using Microsoft.AspNetCore.Mvc;

namespace ONX.CRM.Controllers
{
    public class InformationController : BaseController
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View(); 
        }
        public IActionResult Materials()
        {
            return View();
        }
    }
}
