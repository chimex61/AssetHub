using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AssetModelCategoryId { get; set; }

        [ForeignKey("AssetModelCategoryId")]
        public virtual AssetModelCategory AssetModelCategory { get; set; }

        public virtual ICollection<AssetModelProperty> Properties { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }

    public partial class AssetModelCategory
    {
        public const string NAME_REQUIRED = "Category name is required";

        public const string NAME_EXISTS = "Category with that name already exists";

        public const string SAVE_SUCCESS = "Category is saved successfully";

        public const string SAVE_FAIL = "Category save failed";

        public const string DELETE_SUCCESS = "Category deleted successfully";

        public const string DELETE_FAIL = "Category delete failed";

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }

    public partial class AssetModelProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }
}