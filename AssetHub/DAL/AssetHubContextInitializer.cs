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

            var assetModelsList = new List<AssetModel>()
            {
                new AssetModel {
                    Name = "Computer",
                    AssetModelCategoryId = 1,
                },
                new AssetModel {
                    Name = "Processor",
                    AssetModelCategoryId = 1,
                },
            };

            var assetModelPropertiesList = new List<AssetModelProperty>()
            {
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[0] },
                    Name = "Number of cores",
                    IsNumeric = true,
                },
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[0] },
                    Name = "L3 cache size",
                    IsNumeric = false,
                },
                new AssetModelProperty
                {
                    AssetModels = new List<AssetModel>() { assetModelsList[0] },
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

            //var assetpropertieslist = new list<assetproperty>()
            //{
            //    new assetproperty { asset = assetlist[0], value = "intel core i7-4702mq", assetmodelpropertyid = 4 },
            //    new assetproperty { asset = assetlist[1], value = "4", assetmodelpropertyid = 1 },
            //    new assetproperty { asset = assetlist[1], value = "6", assetmodelpropertyid = 2 },
            //    new assetproperty { asset = assetlist[1], value = "fcpga946", assetmodelpropertyid = 3 },
            //};

            //context.AssetProperties.AddRange(assetPropertiesList);

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