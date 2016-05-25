using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset.Partial
{
    public class AddAssetViewModel
    {
        public class PropertyEditor
        {
            public int PropertyId { get; set; }

            public string Name { get; set; }

            public bool IsNumeric { get; set; }

            public string Value { get; set; }
        }

        public AddAssetViewModel()
        {
            SelectedAssetModelId = -1;
            SelectedRoomId = -1;
            Properties = new List<PropertyEditor>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public int SelectedAssetModelId { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public List<PropertyEditor> Properties { get; set; }

        [Display(Name = "Room")]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

    }
}