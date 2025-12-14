using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

//model with some validation rules
namespace travelapp.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(30, ErrorMessage = "Max 30 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format (must contain @)")]
        public override string Email { get; set; }

    }
}
