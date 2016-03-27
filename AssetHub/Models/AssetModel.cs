using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetModel
    {
        public int AssetModelId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual AssetModelCategory AssetModelCategory { get; set; }

        public virtual IEnumerable<AssetModelProperty> Properties { get; set; }
    }

    public partial class AssetModelCategory
    {
        public int AssetModelCategoryId { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<AssetModel> AssetModels { get; set; }
    }

    public partial class AssetModelProperty
    {
        public int AssetModelPropertyId { get; set; }

        public string Name { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }
    }
}