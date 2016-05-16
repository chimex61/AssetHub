﻿using AssetHub.DAL;
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
            SelectedAssetModelId = -1;
            SelectedCategoryId = -1;
            SearchResults = new List<Models.Asset>();
            AssetModels = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
        }

        public string Name { get; set; }

        [Display(Name ="Serial Key")]
        public string SerialKey { get; set; }

        public int SelectedAssetModelId { get; set; }

        public IEnumerable<SelectListItem> AssetModels { get; set; }

        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<Models.Asset> SearchResults { get; set; }
    }
}