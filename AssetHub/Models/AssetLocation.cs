using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class AssetLocation
    {
        [Column("AssetLocationId")]
        public int Id { get; set; }

        [Column("AssetLocationTimeFrom")]
        public DateTime TimeFrom { get; set; }

        [Column("AssetLocationTimeTo")]
        public DateTime TimeTo { get; set; }

        [Column("AssetLocationAssetId")]
        public int AssetId { get; set; }

        [Column("AssetLocationRoomId")]
        public int RoomId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }
    }
}