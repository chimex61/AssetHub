using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetHub.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public int? UserPositionId { get; set; }

        public int? RoomId { get; set; }

        public virtual UserPosition UserPosition { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }
    }
}