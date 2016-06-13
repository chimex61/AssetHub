using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Category
{
    public class ViewCategoryViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public ViewCategoryViewModel(int id)
        {
            var category = db.AssetModelCategories.Find(id);

            Id = category.Id;
            Name = category.Name;
            AssetModels = category.AssetModels.ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Models.AssetModel> AssetModels { get; set; }
    }
}