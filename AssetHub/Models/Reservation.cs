using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public string UserId{ get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<ReservationAssetUse> ReservationAssetUses { get; set; }
    }
}