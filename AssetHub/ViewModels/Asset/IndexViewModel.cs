using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset
{
    public class IndexViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public IndexViewModel()
        {
            SelectedAssetModel = -1;
            SelectedCategory = -1;
            SearchResults = new List<Models.Asset>();
        }

        public string Name { get; set; }

        [Display(Name ="Serial Key")]
        public string SerialKey { get; set; }

        public int SelectedAssetModel { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public int SelectedCategory { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<Models.Asset> SearchResults { get; set; }
    }
}