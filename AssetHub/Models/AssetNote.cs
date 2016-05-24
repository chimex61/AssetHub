namespace AssetHub.Models
{
    public partial class AssetNote
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int AssetId { get; set; }

        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }
    }
}