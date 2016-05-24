using System.Collections.Generic;

namespace AssetHub.Models
{
    public partial class UserPosition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}