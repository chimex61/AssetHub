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
        [Column("UserFirstName")]
        public string FirstName { get; set; }

        [Column("UserLastName")]
        public string LastName { get; set; }

        [Column("UserUserPositionId")]
        public int UserPositionId { get; set; }

        [Column("UserRoomId")]
        public int RoomId { get; set; }

        public virtual UserPosition UserPosition { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }
    }

    public partial class UserPosition
    {
        [Column("UserPositionId")]
        public int Id { get; set; }

        [Column("UserPositionName")]
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}