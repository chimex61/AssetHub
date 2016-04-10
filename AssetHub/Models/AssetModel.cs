using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetModel
    {
        [Column("AssetModelId")]
        public int Id { get; set; }

        [Column("AssetModelName")]
        public string Name { get; set; }

        [Column("AssetModelCategoryId")]
        public int AssetModelCategoryId { get; set; }

        public virtual ICollection<AssetModelProperty> Properties { get; set; }

        public virtual AssetModelCategory AssetModelCategory { get; set; }
    }

    public partial class AssetModelCategory
    {
        [Column("AssetModelCategoryId")]
        public int Id { get; set; }

        [Column("AssetModelCategoryName")]
        public string Name { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }

    public partial class AssetModelProperty
    {
        [Column("AssetModelPropertyId")]
        public int Id { get; set; }

        [Column("AssetModelPropertyName")]
        public string Name { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }
}