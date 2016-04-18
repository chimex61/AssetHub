using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.AssetModel
{
    public class ViewCategoryViewModel
    {
        public ViewCategoryViewModel(int id)
        {
            using (var db = new AssetHubContext())
            {
                var category = db.AssetModelCategories.Find(id);

                CategoryId = category.Id;
                Name = category.Name;
                AssetModels = category.AssetModels.ToList();
            }
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<Models.AssetModel> AssetModels { get; set; }
    }
}