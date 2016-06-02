using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Admin.Partial
{
    public class EditUserViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public EditUserViewModel()
        {
            ;
        }

        public EditUserViewModel(string id)
        {
            var user = db.Users.Find(id);

            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            IsAdmin = user.IsAdmin;
            SelectedPositionId = user.UserPositionId.HasValue ? user.UserPositionId.Value : -1;
            SelectedRoomId = user.RoomId.HasValue ? user.RoomId.Value : -1;
            Rooms = db.RoomDropdown();
            Positions = db.UserPositionDropdown();
        }

        public string Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = Models.User.Validator.FIRST_NAME_REQUIRED)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = Models.User.Validator.LAST_NAME_REQUIRED)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Models.User.Validator.EMAIL_REQUIRED)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = Models.User.Validator.POSITION_REQUIRED)]
        public int SelectedPositionId { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = Models.User.Validator.ROOM_REQUIRED)]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}