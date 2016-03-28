using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetLocation
    {
        public int AssetLocationId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<AssetUse> AssetUses { get; set; }
    }
}