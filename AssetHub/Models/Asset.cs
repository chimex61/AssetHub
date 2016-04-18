using System;
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

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }

    public class AssetProperty
    {
        [Column("AssetPropertyId")]
        public int Id { get; set; }

        [Column("AssetPropertyValue")]
        public string Value { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetModelProperty AssetModelProperty { get; set; }
    }

    public partial class AssetLocation
    {
        [Column("AssetLocationId")]
        public int Id { get; set; }

        [Column("AssetLocationTimeFrom")]
        public DateTime TimeFrom { get; set; }

        [Column("AssetLocationTimeTo")]
        public DateTime? TimeTo { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }
    }
}