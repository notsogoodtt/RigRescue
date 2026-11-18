using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RigRescue.Data; 
using RigRescue.Models;

namespace RigRescue.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 

        public ClientController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard(string searchLocation)
        {
            var activeTechsQuery = _context.TechnicianProfiles
                                           .Where(t => t.IsVerified && t.IsActive);

            if (!string.IsNullOrEmpty(searchLocation))
            {
                var searchLower = searchLocation.ToLower();
                activeTechsQuery = activeTechsQuery.Where(t => t.Location.ToLower().Contains(searchLower));
            }

            var technicians = await activeTechsQuery.ToListAsync();

            var userId = _userManager.GetUserId(User);
            var myRequests = await _context.ServiceRequests
                                           .Where(r => r.ClientId == userId)
                                           .ToListAsync();

            ViewBag.MyRequests = myRequests;

            return View(technicians);
        }

        [HttpPost]
        public async Task<IActionResult> RequestConsult(int technicianId, string issueDescription)
        {
            var clientName = User.Identity?.Name ?? "Client";

            var newRequest = new ServiceRequest
            {
                ClientId = _userManager.GetUserId(User),
                ClientName = clientName.Split('@')[0], 
                TechnicianProfileId = technicianId,
                IssueDescription = issueDescription,
                Status = "Consult Request",
                RequestDate = DateTime.Now
            };

            _context.ServiceRequests.Add(newRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(int requestId)
        {
            var userId = _userManager.GetUserId(User);
            var requestToDelete = await _context.ServiceRequests.FindAsync(requestId);

            if (requestToDelete != null && requestToDelete.ClientId == userId)
            {
                _context.ServiceRequests.Remove(requestToDelete); 
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Dashboard");
        }
    }
}