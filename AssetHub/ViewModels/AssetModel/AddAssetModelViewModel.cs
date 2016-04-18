using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.AssetModel
{
    public class AddAssetModelViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string[] Property { get; set; }
    }
}