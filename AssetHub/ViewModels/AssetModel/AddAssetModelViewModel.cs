using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetModel
{
    public class AddAssetModelViewModel
    {
        public class PropertyEditor
        {
            public string Name { get; set; }
            public bool IsNumeric { get; set; }
        }

        public AddAssetModelViewModel()
        {
            SelectedCategoryId = -1;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<PropertyEditor> Properties { get; set; }
    }
}