using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Room
    {
        [Column("RoomId")]
        public int Id { get; set; }

        [Column("RoomName")]
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }

}