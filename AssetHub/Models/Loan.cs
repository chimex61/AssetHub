using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Loan
    {
        public int LoanId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<LoanAssetUse> LoanAssetUses { get; set; }
    }
}