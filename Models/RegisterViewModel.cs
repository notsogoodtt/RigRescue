using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RigRescue.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        public string Role { get; set; } // client or technician

        [Required(ErrorMessage = "Location is required for matchmaking.")]
        [Display(Name = "City or Barangay")]
        public string Location { get; set; }

        [Display(Name = "Upload Student ID (Technicians Only)")]
        public IFormFile StudentIdImage { get; set; }
    }
}
