using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetHub.Models
{
    public partial class AssetModelProperty
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsNumeric { get; set; }

        public int? AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }

        public virtual ICollection<AssetProperty> AssetProperties { get; set; }
    }
}