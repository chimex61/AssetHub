using System.Data.Entity;
using AssetHub.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssetHub.DAL
{
    public class AssetHubContext : IdentityDbContext<User>
    {
        static AssetHubContext()
        {
            Database.SetInitializer(new AssetHubContextInitializer());
        }

        public AssetHubContext() : base("name=AssetHubContext") { }

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<AssetNote> AssetNotes { get; set; }

        public virtual DbSet<AssetProperty> AssetProperties { get; set; }

        public virtual DbSet<AssetModel> AssetModels { get; set; }

        public virtual DbSet<AssetModelCategory> AssetModelCategories { get; set; }

        public virtual DbSet<AssetModelProperty> AssetModelProperties { get; set; }

        public virtual DbSet<AssetUse> AssetUses { get; set; }

        public virtual DbSet<LoanAssetUse> LoanAssetUses { get; set; }

        public virtual DbSet<ReservationAssetUse> ReservationAssetUses { get; set; }

        public virtual DbSet<Loan> Loans { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<PersonLocation> PersonLocations { get; set; }

        public virtual DbSet<RoomLocation> RoomLocations { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public static AssetHubContext Create() => new AssetHubContext();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().HasRequired(f => f.Asset)
                .WithRequiredDependent()
                .WillCascadeOnDelete(false);
        }
    }
}