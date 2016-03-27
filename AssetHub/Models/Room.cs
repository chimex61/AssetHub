using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Room
    {
        public int RoomId { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }

}