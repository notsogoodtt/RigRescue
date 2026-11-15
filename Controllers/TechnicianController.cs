using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RigRescue.Controllers
{
    [Authorize]
    public class TechnicianController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}