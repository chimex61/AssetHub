using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset
{
    public class AddAssetViewModel
    {
        public class PropertyEditor
        {
            public int ModelId { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }

        public AddAssetViewModel()
        {
            using (var db = new AssetHubContext())
            {
                SelectedAssetModelId = -1;
                SelectedRoomId = -1;
                AssetModels = db.AssetModelDropdown().ToList();
                Rooms = db.RoomDropdown().ToList();
                Properties = new List<PropertyEditor>();
            }
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public int SelectedAssetModelId { get; set; }

        [Display(Name = "Room")]
        public int SelectedRoomId { get; set; }

        public List<PropertyEditor> Properties { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

    }
}