using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetModel
    {
        public const string NAME_REQUIRED = "Model name is required";

        public const string NAME_EXISTS = "Model with that name in this category already exists";

        public const string CATEGORY_REQUIRED = "Model category is required";

        public const string PROPERTY_NAME_REQUIRED = "Property name is required";

        public const string SAVE_SUCCESS = "Model is saved successfully";

        public const string SAVE_FAIL = "Model save failed";

        public int Id { get; set; }

        public string Name { get; set; }

        public int AssetModelCategoryId { get; set; }

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

        public string Expression { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }
    }
}