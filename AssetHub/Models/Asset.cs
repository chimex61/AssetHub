using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int AssetModelId { get; set; }

        [ForeignKey("AssetModelId")]
        public virtual AssetModel AssetModel { get; set; }

        public virtual ICollection<AssetProperty> AssetProperties { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }

        public virtual ICollection<AssetLocation> Locations { get; set; }
    }

    public partial class AssetNote
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int AssetId { get; set; }

        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }

    public class AssetProperty
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int AssetId { get; set; }

        public int AssetModelPropertyId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetModelProperty AssetModelProperty { get; set; }
    }

    public partial class AssetLocation
    {
        public int Id { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public int AssetId { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("AssetId")]
        public virtual Asset Asset { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}