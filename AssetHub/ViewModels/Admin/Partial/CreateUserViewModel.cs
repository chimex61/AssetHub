using AssetHub.DAL;
using AssetHub.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Admin.Partial
{
    public class CreateUserViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public CreateUserViewModel()
        {
            SelectedPositionId = -1;
            SelectedRoomId = -1;
            Positions = db.UserPositionDropdown();
            Rooms = db.RoomDropdown();
        }

        [Required(ErrorMessage = User.Validator.FIRST_NAME_REQUIRED)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = User.Validator.LAST_NAME_REQUIRED)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = User.Validator.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = User.Validator.INVALID_EMAIL)]
        public string Email { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdministrator { get; set; }

        [Required(ErrorMessage = User.Validator.PASSWORD_REQUIRED)]
        [RegularExpression(User.Validator.PASSWORD_REGEX, ErrorMessage = User.Validator.INVALID_PASSWORD)]
        public string Password { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = User.Validator.POSITION_REQUIRED)]
        public int SelectedPositionId { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = User.Validator.ROOM_REQUIRED)]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}