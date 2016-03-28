using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public abstract partial class AssetUse
    {
        public int AssetUseId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int AssetId { get; set; }

        public string UserId { get; set; }

        public int AssetLocationId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }

        public virtual AssetLocation AssetLocation { get; set; }
    }

    [Table("Reservations")]
    public partial class Reservation : AssetUse
    {

    }

    [Table("Loans")]
    public partial class Loan : AssetUse
    {
        public bool IsReturned { get; set; }
    }
}