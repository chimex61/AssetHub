﻿using System.ComponentModel.DataAnnotations;

namespace AssetHub.Models
{
    public class AssetProperty
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        public int AssetId { get; set; }

        public int AssetModelPropertyId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetModelProperty AssetModelProperty { get; set; }
    }
}