using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            SelectedCategoryId = -1;
            Categories = new List<SelectListItem>();
            SearchResults = new List<Models.AssetModel>();
        }

        public string Name { get; set; }

        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<Models.AssetModel> SearchResults { get; set; }
    }
}