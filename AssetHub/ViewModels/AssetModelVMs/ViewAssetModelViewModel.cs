using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.AssetModelVMs
{
    public class ViewAssetModelViewModel
    {
        public ViewAssetModelViewModel(int id)
        {
            using (var db = new AssetHubContext())
            {
                var assetModel = db.AssetModels.Find(id);

                Id = assetModel.Id;
                Name = assetModel.Name;
                Category = assetModel.AssetModelCategory;
                Properties = assetModel.Properties.ToList();
                Assets = assetModel.Assets.ToList();
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public AssetModelCategory Category { get; set; }

        public ICollection<AssetModelProperty> Properties { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}