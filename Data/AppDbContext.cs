using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RigRescue.Models;

namespace RigRescue.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TechnicianProfile> TechnicianProfiles { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

    }
}