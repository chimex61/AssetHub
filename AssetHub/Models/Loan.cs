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

        public int UserAcountId { get; set; }

        public virtual UserAccount UserAccount { get; set; }

        public virtual IEnumerable<LoanAssetUse> LoanAssetUses { get; set; }
    }
}