using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset
    {
        public class Validator
        {

        }

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
}