using System;
using System.ComponentModel.DataAnnotations;
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

        public int RoomId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }

        public virtual Room Room { get; set; }
    }
}