using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Category
{
    public class IndexViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public IndexViewModel()
        {
            Categories = db.AssetModelCategories.ToList();
        }

        public List<Models.AssetModelCategory> Categories { get; set; }
    }
}