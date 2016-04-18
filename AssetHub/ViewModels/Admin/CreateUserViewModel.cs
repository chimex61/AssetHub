using System.ComponentModel.DataAnnotations;

namespace AssetHub.ViewModels.Admin
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^.{6,}", ErrorMessage = "Password length must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Room")]
        public string Room { get; set; }
    }
}