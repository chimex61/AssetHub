using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AssetHub.Models
{
    public partial class AssetModel
    {
        public class Validator
        {
            public const string NAME_REQUIRED = "Model name is required";

            public const string NAME_EXISTS = "Model with that name in this category already exists";

            public const string CATEGORY_REQUIRED = "Model category is required";

            public const string PROPERTY_NAME_REQUIRED = "Property name is required";

            public const string PROPERTY_NAME_DUPLICATE = "Duplicate property names found";

            public static string ValidateCategory(int id)
            {
                return id < 0 ? CATEGORY_REQUIRED : null;
            }

            public static string ValidateName(int? id, string name, int categoryId)
            {
                using (var db = new AssetHubContext())
                {
                    if(string.IsNullOrWhiteSpace(name))
                    {
                        return NAME_REQUIRED;
                    }

                    var existing = (from c in db.AssetModelCategories
                                    join m in db.AssetModels on c.Id equals m.AssetModelCategoryId
                                    where m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) && m.AssetModelCategoryId != categoryId
                                    select m).Count();

                    if(existing != 0)
                    {
                        return NAME_EXISTS;
                    }
                }

                return null;
            }

            public static string ValidateProperty(string name)
            {
                return string.IsNullOrWhiteSpace(name) ? PROPERTY_NAME_REQUIRED : null;
            }

            public static string ValidateProperties(ICollection<Tuple<string, bool>> properties)
            {
                var count = (from p in properties
                             select new { Name = p.Item1.ToLower() }).Distinct().Count();

                if(count != properties.Count) { return PROPERTY_NAME_DUPLICATE; }
                return null;
            }
        }

        public const string SAVE_SUCCESS = "Model is saved successfully";

        public const string SAVE_FAIL = "Model save failed";

        public const string DELETE_SUCCESS = "Model is deleted successfully";

        public const string DELETE_FAIL = "Model delete failed";

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int AssetModelCategoryId { get; set; }

        public virtual AssetModelCategory AssetModelCategory { get; set; }

        public virtual ICollection<AssetModelProperty> Properties { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}