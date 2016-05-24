using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Category.Partial
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = AssetModelCategory.Validator.NAME_REQUIRED)]
        public string Name { get; set; }
    }
}