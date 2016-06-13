using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset.Partial
{
    public class ChangeLocationViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public ChangeLocationViewModel()
        {
            SelectedRoomId = -1;
            Rooms = db.RoomDropdown();
        }

        public ChangeLocationViewModel(int assetId, int roomId) : this()
        {
            AssetId = assetId;
            SelectedRoomId = roomId;
        }

        public int AssetId { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Room is required")]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}