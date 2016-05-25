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

            [Required(ErrorMessage = Models.Asset.Validator.PROPERTY_VALUE_REQUIRED)]
            public string Value { get; set; }
        }

        public AddAssetViewModel()
        {
            SelectedAssetModelId = -1;
            SelectedRoomId = -1;
            Properties = new List<PropertyEditor>();
        }

        [Required(ErrorMessage = Models.Asset.Validator.NAME_REQUIRED)]
        public string Name { get; set; }

        [Required(ErrorMessage = Models.Asset.Validator.SERIAL_REQUIRED)]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        [Required(ErrorMessage = Models.Asset.Validator.MODEL_REQUIRED)]
        public int SelectedAssetModelId { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public List<PropertyEditor> Properties { get; set; }

        [Display(Name = "Room")]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

    }
}