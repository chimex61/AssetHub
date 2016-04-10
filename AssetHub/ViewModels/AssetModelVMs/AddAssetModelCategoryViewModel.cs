using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.AssetModelVMs
{
    public class AddAssetModelCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}