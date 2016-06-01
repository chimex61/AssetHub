using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Admin.Partial
{
    public class SearchViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public SearchViewModel()
        {
            //SelectedPositionId = -1;
            //Positions = db.UserPositionDropdown();
            SelectedRoomId = -1;
            Rooms = db.RoomDropdown();
        }

        public string Name { get; set; }

        //[Display(Name = "With position")]
        //public int SelectedPositionId { get; set; }

        [Display(Name = "In room")]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}