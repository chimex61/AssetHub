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

        public int LocationId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Location Location { get; set; }
    }

    public partial class LoanAssetUse : AssetUse
    {
        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; }
    }

    public partial class ReservationAssetUse : AssetUse
    {
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}