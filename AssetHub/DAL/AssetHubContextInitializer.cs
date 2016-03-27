using AssetHub.Infrastructure;
using AssetHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssetHub.DAL
{
    public class AssetHubContextInitializer : DropCreateDatabaseAlways<AssetHubContext>
    {
        protected override void Seed(AssetHubContext context)
        {
            PreformInitialSetup(context);
            base.Seed(context);
        }

        private void PreformInitialSetup(AssetHubContext context)
        {
            AddRooms(context);
            AddPositions(context);
            AddUsers(context);
        }

        private void AddRooms(AssetHubContext context)
        {
            var rooms = new List<Room>
            {
                new Room { Name = "PCLAB2" },
                new Room { Name = "B4" },
                new Room { Name = "B5" },
                new Room { Name = "A209" },
                new Room { Name = "C15" },
                new Room { Name = "A202" },
            };

            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }

        private void AddPositions(AssetHubContext context)
        {
            var positions = new List<Position>
            {
                new Position { Name = "Product Manager" },
                new Position { Name = "Integration Developer" },
                new Position { Name = "Lead Developer" },
                new Position { Name = "Data Scientist" },
                new Position { Name = "Scrum Master" },
                new Position { Name = "Software Engineer" },
            };

            context.Positions.AddRange(positions);
            context.SaveChanges();
        }

        private void AddUsers(AssetHubContext context)
        {
            var manager = new AssetHubUserManager(new UserStore<User>(context));

            var userList = new List<User>()
            {
                new User { Name = "Jan Kelemen", UserName = "jan.kelemen@asset.hub",  Email = "jan.kelemen@asset.hub", PositionId = 1, RoomId = 6 },
                new User { Name = "Borna Skukan", UserName = "borna.skukan@asset.hub", Email = "borna.skukan@asset.hub", PositionId = 2, RoomId = 5 },
                new User { Name = "Ivan Brezovec", UserName = "ivan.brezovec@asset.hub", Email = "ivan.brezovec@asset.hub", PositionId = 3, RoomId = 4 },
                new User { Name = "Marijana Krivić", UserName = "marijana.krivic@asset.hub", Email = "marijana.krivic@asset.hub", PositionId = 4, RoomId = 3 },
                new User { Name = "Marina Brebrić", UserName = "marina.brebric@asset.hub", Email = "marina.brebric@asset.hub", PositionId = 5, RoomId = 2 },
                new User { Name = "Filip Gulan", UserName = "filip.gulan@asset.hub", Email = "filip.gulan@asset.hub", PositionId = 6, RoomId = 1 },
            };

            foreach (var user in userList)
            {
                manager.Create(user, user.Name);
            }

            context.SaveChanges();
        }
    }
}