using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetVMs
{
    public class AddAssetViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public int SelectedAssetModel { get; set; }

        public string[] PropertyValue { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }
    }
}