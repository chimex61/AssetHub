using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetModel.Partial
{
    public class SearchViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public SearchViewModel()
        {
            SelectedCategoryId = -1;
            Categories = db.CategoryDropdown();
        }

        public string Name { get; set; }

        [Display(Name = "Category")]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}