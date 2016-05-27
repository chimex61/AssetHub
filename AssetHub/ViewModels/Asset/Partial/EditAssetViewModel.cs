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
    public class EditAssetViewModel
    {
        public class PropertyEditor
        {
            public int ModelPropertyId { get; set; }

            public string Name { get; set; }

            public bool IsNumeric { get; set; }

            public int AssetPropertyId { get; set; }

            [Required(ErrorMessage = Models.Asset.Validator.PROPERTY_VALUE_REQUIRED)]
            public string Value { get; set; }
        }

        AssetHubContext db = new AssetHubContext();

        public EditAssetViewModel() { }

        public EditAssetViewModel(int id)
        {
            var asset = db.Assets.Find(id);

            Id = asset.Id;
            Name = asset.Name;
            SerialNumber = asset.SerialNumber;
            SelectedAssetModelId = asset.AssetModelId;
            AssetModels = db.AssetModelDropdown();

            Properties = new List<PropertyEditor>();
            foreach(var p in asset.AssetProperties)
            {
                Properties.Add(new PropertyEditor
                {
                    ModelPropertyId = p.AssetModelPropertyId,
                    Name = p.AssetModelProperty.Name,
                    IsNumeric = p.AssetModelProperty.IsNumeric,
                    AssetPropertyId = p.Id,
                    Value = p.Value,
                });
            }
        }

        public int Id { get; set; }

        [Required(ErrorMessage = Models.Asset.Validator.NAME_REQUIRED)]
        public string Name { get; set; }

        [Display(Name = "Serial number")]
        [Required(ErrorMessage = Models.Asset.Validator.SERIAL_REQUIRED)]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        [Required(ErrorMessage = Models.Asset.Validator.MODEL_REQUIRED)]
        public int SelectedAssetModelId { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public List<PropertyEditor> Properties { get; set; }
    }
}