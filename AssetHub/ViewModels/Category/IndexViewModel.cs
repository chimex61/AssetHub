using AssetHub.Models;
using System;
using System.Collections.Generic;
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

        public string Name { get; set; }

        public List<AssetModelCategory> Categories { get; set; }
    }
}