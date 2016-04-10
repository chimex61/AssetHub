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
            addRooms(context);
            addUserPositions(context);
            addUsers(context);
            addAssetModelCategories(context);
            addAssetModels(context);
        }

        private void addRooms(AssetHubContext context)
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

        private void addUserPositions(AssetHubContext context)
        {
            var userPositions = new List<UserPosition>
            {
                new UserPosition { Name = "Product Manager" },
                new UserPosition { Name = "Integration Developer" },
                new UserPosition { Name = "Lead Developer" },
                new UserPosition { Name = "Data Scientist" },
                new UserPosition { Name = "Scrum Master" },
                new UserPosition { Name = "Software Engineer" },
            };

            context.UserPositions.AddRange(userPositions);
            context.SaveChanges();
        }

        private void addUsers(AssetHubContext context)
        {
            var manager = new AssetHubUserManager(new UserStore<User>(context));

            var userList = new List<User>()
            {
                new User { FirstName = "Jan", LastName = "Kelemen", UserName = "jan.kelemen@asset.hub",  Email = "jan.kelemen@asset.hub", UserPosition = context.FindOrAddUserPosition("Product Manager"), Room = context.FindOrAddRoom("PCLAB2") },
            };

            foreach (var user in userList)
            {
                var result = manager.CreateAsync(user, "0123456789").Result;
                Console.WriteLine(result.Errors);
            }

            context.SaveChanges();
        }

        private void addAssetModelCategories(AssetHubContext context)
        {
            var categoriesList = new List<AssetModelCategory>()
            {
                new AssetModelCategory { Name = "IT equipment" },
                new AssetModelCategory { Name = "Office equipment" },
            };

            context.AssetModelCategories.AddRange(categoriesList);
            context.SaveChanges();
        }

        private void addAssetModels(AssetHubContext context)
        {
            var propertiesList = new List<AssetModelProperty>()
            {
                new AssetModelProperty { Name = "Number of cores" },
                new AssetModelProperty { Name = "L3 cache size" },
                new AssetModelProperty { Name = "Socket" },
                new AssetModelProperty { Name = "Processor" },
                new AssetModelProperty { Name = "Ink color" },
            };

            context.AssetModelProperties.AddRange(propertiesList);

            var assetModelsList = new List<AssetModel>()
            {
                new AssetModel {
                    Name = "Computer",
                    AssetModelCategory = context.FindOrAddAssetModelCategory("IT equipment"),
                    Properties = new List<AssetModelProperty>() { propertiesList[3] },
                },
                new AssetModel {
                    Name = "Processor",
                    AssetModelCategory = context.FindOrAddAssetModelCategory("IT equipment"),
                    Properties = new List<AssetModelProperty>() { propertiesList[0], propertiesList[1], propertiesList[2] },
                },
            };

            context.AssetModels.AddRange(assetModelsList);

            context.SaveChanges();
        }
    }
}