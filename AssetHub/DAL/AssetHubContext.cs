using System.Data.Entity;
using AssetHub.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AssetHub.DAL
{
    public class AssetHubContext : IdentityDbContext<UserAccount>
    {
        static AssetHubContext()
        {
            Database.SetInitializer(new AssetHubContextInitializer());
        }

        public AssetHubContext()
            : base("name=AssetHubContext")
        {
        }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public static AssetHubContext Create() => new AssetHubContext();
    }
}