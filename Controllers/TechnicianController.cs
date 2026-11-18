using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RigRescue.Models;
using RigRescue.Data;

namespace RigRescue.Controllers
{
    [Authorize]
    public class TechnicianController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public TechnicianController(AppDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            var myProfile = await _context.TechnicianProfiles.FirstOrDefaultAsync(t => t.UserId == userId);

            if (myProfile == null)
            {
                return RedirectToAction("SetupProfile");
            }

            var incomingRequests = await _context.ServiceRequests
                                                 .Where(r => r.TechnicianProfileId == myProfile.Id)
                                                 .ToListAsync();

            var clientAddresses = new Dictionary<string, string>();
            foreach (var req in incomingRequests)
            {
                if (!clientAddresses.ContainsKey(req.ClientId))
                {
                    var clientUser = await _userManager.FindByIdAsync(req.ClientId);
                    clientAddresses[req.ClientId] = clientUser?.Location ?? "Address not found";
                }
            }
            ViewBag.ClientAddresses = clientAddresses; 

            ViewBag.TechnicianName = myProfile.Name;
            ViewBag.IsActive = myProfile.IsActive;
            return View(incomingRequests);
        }

        [HttpGet]
        public IActionResult SetupProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetupProfile(string name, string specialty, IFormFile studentIdFile)
        {
            if (studentIdFile == null || studentIdFile.Length == 0)
            {
                ModelState.AddModelError("", "You must upload a valid Student ID to continue.");
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User); 

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + studentIdFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await studentIdFile.CopyToAsync(fileStream);
            }

            var profile = new TechnicianProfile
            {
                UserId = currentUser.Id,
                Name = name,
                Location = currentUser.Location ?? "No location provided",
                Specialty = specialty,
                StudentIdPath = uniqueFileName,
                IsVerified = true,
                IsActive = false
            };

            _context.TechnicianProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(bool isActive)
        {
            var userId = _userManager.GetUserId(User);
            var profile = await _context.TechnicianProfiles.FirstOrDefaultAsync(t => t.UserId == userId);

            if (profile != null)
            {
                profile.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }
    }
}