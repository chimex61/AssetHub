using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }

        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }

    }

    public partial class RoomLocation : Location
    {
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }

    public partial class PersonLocation : Location
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}