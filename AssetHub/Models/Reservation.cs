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

        public int AccountId { get; set; }

        public virtual UserAccount UserAccouunt { get; set; }

        public virtual IEnumerable<ReservationAssetUse> ReservationAssetUses { get; set; }
    }
}