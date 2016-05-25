using System.Collections.Generic;

namespace AssetHub.Models
{
    public partial class AssetModelProperty
    {
        public AssetModelProperty() { AssetModels = new List<AssetModel>(); }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsNumeric { get; set; }

        public virtual ICollection<AssetModel> AssetModels { get; set; }
    }
}