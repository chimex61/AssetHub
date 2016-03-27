using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Position
    {
        public int PositionId { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}