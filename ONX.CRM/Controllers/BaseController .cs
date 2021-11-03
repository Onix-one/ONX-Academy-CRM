using Microsoft.AspNetCore.Mvc;

namespace ONX.CRM.Controllers
{
    public class BaseController : Controller
    {
        public static string Theme { get; set; }
        public static byte[] Photo { get; set; }
       
        public BaseController() { }
       
        public IActionResult ChangeTheme(int themeId)
        {
            const int darkThemeId = 0;
            const int lightThemeId = 1;
            const int semiDarkThemeId = 2;

            if (themeId == darkThemeId)
            {
                Theme = "dark-theme";
            }
            if (themeId == lightThemeId)
            {
                Theme = "light-theme";
            }
            if (themeId == semiDarkThemeId)
            {
                Theme = "semi-dark";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
