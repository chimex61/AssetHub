using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.AssetModel
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

        public List<AssetModelProperty> Properties { get; set; }

        public ICollection<Models.Asset> Assets { get; set; }
    }
}