using System.ComponentModel.DataAnnotations;

namespace AssetHub.ViewModels.AdminVMs
{
    public class CreateUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Room { get; set; }
    }
}