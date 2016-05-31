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
            addUsers(context);
            addAssets(context);
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

        private void addUsers(AssetHubContext context)
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

            var manager = new AssetHubUserManager(new UserStore<User>(context));

            var userList = new List<User>()
            {
                new User
                {
                    FirstName = "Jan",
                    LastName = "Kelemen",
                    UserName = "jan@asset.hub",
                    IsAdmin = true,
                    Email = "jan.kelemen@asset.hub",
                    UserPosition = context.FindOrAddUserPosition("Product Manager"),
                    Room = context.FindOrAddRoom("PCLAB2")
                },

                new User
                {
                    FirstName = "Filip",
                    LastName = "Gulan",
                    UserName = "filp@asset.hub",
                    IsAdmin = false,
                    Email = "filip@asset.hub",
                    UserPosition = context.FindOrAddUserPosition("Software Engineer"),
                    Room = context.FindOrAddRoom("B5")
                }
            };

            foreach (var user in userList)
            {
                var result = manager.CreateAsync(user, "0123456789").Result;
            }

            context.SaveChanges();
        }

        private void addAssets(AssetHubContext context)
        {
            var categoriesList = new List<AssetModelCategory>()
            {
                new AssetModelCategory { Name = "IT equipment" },
            };

            context.AssetModelCategories.AddRange(categoriesList);
            context.SaveChanges();

            var assetModelsList = new List<AssetModel>()
            {
                new AssetModel {
                    Name = "Computer",
                    AssetModelCategory = categoriesList[0],
                },
                new AssetModel {
                    Name = "Processor",
                    AssetModelCategory = categoriesList[0],
                },
            };

            var assetModelPropertiesList = new List<AssetModelProperty>()
            {
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[1] },
                    Name = "Number of cores",
                    IsNumeric = true,
                },
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[1] },
                    Name = "L3 cache size",
                    IsNumeric = false,
                },
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[1] },
                    Name = "Socket",
                    IsNumeric = false,
                },
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[0] },
                    Name = "Processor",
                    IsNumeric = false,
                },
            };

            context.AssetModelProperties.AddRange(assetModelPropertiesList);
            context.AssetModels.AddRange(assetModelsList);

            var assetList = new List<Asset>()
            {
                new Asset
                {
                    Name = "Acer Aspire V3-772G",
                    SerialNumber = "324017",
                    AssetModel = assetModelsList[0],
                    AssetProperties = new List<AssetProperty>(),
                },

                new Asset
                {
                    Name = "Intel Core i7-4702MQ",
                    SerialNumber = "0123456",
                    AssetModel = assetModelsList[1],
                    AssetProperties = new List<AssetProperty>(),
                },
            };

            var assetPropertiesList = new List<AssetProperty>()
            {
                new AssetProperty
                {
                    Asset = assetList[0],
                    AssetModelProperty = assetModelPropertiesList[3],
                    Value = "Intel core i7-4702mq"
                },

                new AssetProperty
                {
                    Asset = assetList[1],
                    AssetModelProperty = assetModelPropertiesList[0],
                    Value = "84 kB",
                },

                new AssetProperty
                {
                    Asset = assetList[1],
                    AssetModelProperty = assetModelPropertiesList[1],
                    Value = "6",
                },

                new AssetProperty
                {
                    Asset = assetList[1],
                    AssetModelProperty = assetModelPropertiesList[2],
                    Value = "fcpga946",
                }
            };

            var assetLocationList = new List<AssetLocation>()
            {
                new AssetLocation { Asset = assetList[0], RoomId = 1, TimeFrom = DateTime.Now },
                new AssetLocation { Asset = assetList[1], RoomId = 2, TimeFrom = DateTime.Now },
            };

            context.Assets.AddRange(assetList);
            context.AssetProperties.AddRange(assetPropertiesList);
            context.AssetLocations.AddRange(assetLocationList);
            context.SaveChanges();
        }
    }
}