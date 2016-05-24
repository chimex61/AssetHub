namespace AssetHub.Models
{
    public partial class AssetModelProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Expression { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }
    }
}