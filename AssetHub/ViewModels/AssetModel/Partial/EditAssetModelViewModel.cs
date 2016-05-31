using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetModel.Partial
{
    public class EditAssetModelViewModel
    {
        public class PropertyEditor
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public bool IsNumeric { get; set; }

            public bool IsEditable { get; set; }
        }

        AssetHubContext db = new AssetHubContext();

        public EditAssetModelViewModel()
        {
            SelectedCategoryId = -1;
            Categories = db.CategoryDropdown();

            DeletedProperties = new List<PropertyEditor>();
            Properties = new List<PropertyEditor>();
            NewProperties = new List<PropertyEditor>();
        }

        public EditAssetModelViewModel(int id) : this()
        {
            var model = db.AssetModels.Find(id);

            Id = model.Id;
            Name = model.Name;
            SelectedCategoryId = model.AssetModelCategoryId;

            foreach(var p in model.Properties)
            {
                Properties.Add(new PropertyEditor
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsNumeric = p.IsNumeric,
                    IsEditable = p.AssetProperties.Count == 0,
                });
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = Models.AssetModel.Validator.CATEGORY_REQUIRED)]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public List<PropertyEditor> DeletedProperties { get; set; }

        public List<PropertyEditor> Properties { get; set; }

        public List<PropertyEditor> NewProperties { get; set; }
    }
}