using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset 
    {
        public int AssetId { get; set; }

        public string SerialNumber { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }

        public virtual IEnumerable<AssetProperty> AssetProperties { get; set; }

        public virtual IEnumerable<AssetUse> AssetUses { get; set; }

        public virtual IEnumerable<Location> Locations { get; set; }
    }

    public partial class AssetNote
    {
        public int AssetNoteId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int AssetId { get; set; }

        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }

    public class AssetProperty
    {
        public int AssetPropertyId { get; set; }

        public string Value { get; set; }

        public int AssetId { get; set; }

        public int ModelPropetyId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetModelProperty ModelProperty { get; set; }
    }
}