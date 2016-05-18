using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Category
{
    public class ViewCategoryViewModel
    {
        public ViewCategoryViewModel() { AssetModels = new List<Models.AssetModel>(); }

        public ViewCategoryViewModel(int id)
        {
            using (var db = new AssetHubContext())
            {
                var category = db.AssetModelCategories.Find(id);

                Id = category.Id;
                Name = category.Name;
                AssetModels = category.AssetModels.ToList();
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Models.AssetModel> AssetModels { get; set; }
    }
}