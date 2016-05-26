using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset.Partial
{
    public class SearchViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public SearchViewModel()
        {
            SelectedAssetModelId = -1;
            AssetModels = db.AssetModelDropdown();
        }

        public string Name { get; set; }

        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public int SelectedAssetModelId { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }
    }
}