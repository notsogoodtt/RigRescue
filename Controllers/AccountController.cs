using Microsoft.AspNetCore.Mvc;
using RigRescue.ViewModels;

namespace RigRescue.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

      [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Integrate ASP.NET Core Identity SignInManager.
                // TODO: Redirect to Home/Dashboard.
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
