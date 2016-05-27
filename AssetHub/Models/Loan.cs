using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Loan
    {
        public int Id { get; set; }

        public bool IsReturned { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int AssetId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("AssetId")]
        public virtual Asset Asset { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}