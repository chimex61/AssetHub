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
            addAssetModels(context);
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
                new User { FirstName = "Jan", LastName = "Kelemen", UserName = "jan.kelemen@asset.hub",  Email = "jan.kelemen@asset.hub", UserPosition = context.FindOrAddUserPosition("Product Manager"), Room = context.FindOrAddRoom("PCLAB2") },
            };

            foreach (var user in userList)
            {
                var result = manager.CreateAsync(user, "0123456789").Result;
            }

            context.SaveChanges();
        }

        private void addAssetModels(AssetHubContext context)
        {
            var categoriesList = new List<AssetModelCategory>()
            {
                new AssetModelCategory { Name = "IT equipment" },
            };

            context.AssetModelCategories.AddRange(categoriesList);
            context.SaveChanges();

            var propertiesList = new List<AssetModelProperty>()
            {
                new AssetModelProperty { Name = "Number of cores" },
                new AssetModelProperty { Name = "L3 cache size" },
                new AssetModelProperty { Name = "Socket" },
                new AssetModelProperty { Name = "Processor" },
            };

            context.AssetModelProperties.AddRange(propertiesList);
            context.SaveChanges();

            var assetModelsList = new List<AssetModel>()
            {
                new AssetModel {
                    Name = "Computer",
                    AssetModelCategoryId = 1,
                    Properties = new List<AssetModelProperty>() { propertiesList[3] },
                },
                new AssetModel {
                    Name = "Processor",
                    AssetModelCategoryId = 1,
                    Properties = new List<AssetModelProperty>() { propertiesList[0], propertiesList[1], propertiesList[2] },
                },
            };

            context.AssetModels.AddRange(assetModelsList);
            context.SaveChanges();
        }

        private void addAssets(AssetHubContext context)
        {
            var assetList = new List<Asset>()
            {
                new Asset
                {
                    Name = "Acer Aspire V3-772G",
                    SerialNumber = "324017",
                    AssetModelId = 1,
                },

                new Asset
                {
                    Name = "Intel Core i7-4702MQ",
                    SerialNumber = "0123456",
                    AssetModelId = 2,
                },
            };

            context.Assets.AddRange(assetList);

            var assetPropertiesList = new List<AssetProperty>()
            {
                new AssetProperty { Asset = assetList[0], Value = "Intel Core i7-4702MQ", AssetModelPropertyId = 4 },
                new AssetProperty { Asset = assetList[1], Value = "4", AssetModelPropertyId = 1 },
                new AssetProperty { Asset = assetList[1], Value = "6", AssetModelPropertyId = 2 },
                new AssetProperty { Asset = assetList[1], Value = "FCPGA946", AssetModelPropertyId = 3 },
            };

            context.AssetProperties.AddRange(assetPropertiesList);

            var assetLocationList = new List<AssetLocation>()
            {
                new AssetLocation { Asset = assetList[0], RoomId = 1, TimeFrom = DateTime.Now },
                new AssetLocation { Asset = assetList[1], RoomId = 2, TimeFrom = DateTime.Now },
            };

            context.AssetLocations.AddRange(assetLocationList);

            context.SaveChanges();
        }
    }
}