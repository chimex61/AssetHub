using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Category
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Categories = new List<AssetModelCategory>();
        }

        [Required(ErrorMessage = AssetModelCategory.NAME_REQUIRED)]
        public string Name { get; set; }

        public List<AssetModelCategory> Categories { get; set; }
    }
}