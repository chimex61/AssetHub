using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset 
    {
        [Column("AssetId")]
        public int Id { get; set; }

        [Column("AssetName")]
        public string Name { get; set; }

        [Column("AssetSerialNumber")]
        public string SerialNumber { get; set; }

        [Column("AssetAssetModelId")]
        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }

        public virtual IEnumerable<AssetProperty> AssetProperties { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }

        public virtual IEnumerable<AssetLocation> Locations { get; set; }
    }

    public partial class AssetNote
    {
        [Column("AssetNoteId")]
        public int Id { get; set; }

        [Column("AssetNoteTitle")]
        public string Title { get; set; }

        [Column("AssetNoteText")]
        public string Text { get; set; }

        [Column("AssetNoteAssetId")]
        public int AssetId { get; set; }

        [Column("AssetNoteUserId")]
        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }

    public class AssetProperty
    {
        [Column("AssetPropertyId")]
        public int Id { get; set; }

        [Column("AssetPropertyValue")]
        public string Value { get; set; }

        [Column("AssetPropertyAssetId")]
        public int AssetId { get; set; }

        [Column("AssetPropertyAssetModelPropertyId")]
        public int AssetModelPropertyId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetModelProperty AssetModelProperty { get; set; }
    }
}