using System;

namespace AssetHub.Models
{
    public partial class AssetLocation
    {
        public int Id { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public int AssetId { get; set; }

        public int RoomId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Room Room { get; set; }
    }
}