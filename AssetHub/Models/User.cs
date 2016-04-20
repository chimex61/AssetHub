using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetHub.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UserPositionId { get; set; }

        public int RoomId { get; set; }

        [ForeignKey("UserPositionId")]
        public virtual UserPosition UserPosition { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }
    }

    public partial class UserPosition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}