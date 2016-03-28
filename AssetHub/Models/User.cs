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
        public string Name { get; set; }

        public int PositionId { get; set; }

        public int RoomId { get; set; }

        public virtual Position Position { get; set; }

        public virtual Room Room { get; set; }

        public virtual IEnumerable<AssetUse> AssetUses { get; set; }
    }
}