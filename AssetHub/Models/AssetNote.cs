using System.ComponentModel.DataAnnotations;

namespace AssetHub.Models
{
    public partial class AssetNote
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int AssetId { get; set; }

        public virtual Asset Asset { get; set; }

    }
}