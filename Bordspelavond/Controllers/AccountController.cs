using Microsoft.AspNetCore.Mvc;

namespace Bordspelavond.Controllers
{
    public class Account : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult TooYoung()
        {
            return View();
        }
    }
}
