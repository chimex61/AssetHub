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

        public int[] SelectedPropertyId { get; set; }
    }
}