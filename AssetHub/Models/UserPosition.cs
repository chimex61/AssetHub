using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AssetHub.Models
{
    public partial class UserPosition
    {
        public class Validator
        {
            public const string NAME_REQUIRED = "Position name is required";

            public const string NAME_DUPLICATE = "Duplicate position names found";

            public static string ValidateName(string name)
            {
                return string.IsNullOrWhiteSpace(name) ? NAME_REQUIRED : null;
            }

            public static string ValidateNames(ICollection<string> names)
            {
                var count = (from n in names
                             select n.ToLower()).Distinct().Count();

                return count != names.Count ? NAME_DUPLICATE : null;
            }
        }

        public const string SAVE_SUCCESS = "Positions saved successfully";

        public const string SAVE_FAIL = "Positions save failed";

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}