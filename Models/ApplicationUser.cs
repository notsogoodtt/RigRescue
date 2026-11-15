using Microsoft.AspNetCore.Identity;

namespace RigRescue.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Role { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? StudentIDUrl { get; set; }

        public bool IsVerified { get; set; } = false;
    }
}