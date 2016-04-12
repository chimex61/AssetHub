using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.AssetModelVMs;
using AssetHub.ViewModels.AssetVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class AssetController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        // GET: AddAssetModelProperty
        public ActionResult AddAssetModelProperty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAssetModelProperty(AddAssetModelPropertyViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var property = db.FindOrAddAssetModelProperty(vm.Name);
            }

            return View();
        }

        // GET: AddAssetModelCategory
        public ActionResult AddAssetModelCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAssetModelCategory(AddAssetModelCategoryViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var category = db.FindOrAddAssetModelCategory(vm.Name);
            }

            return View();
        }
        

        // GET: AddAssetModel
        public ActionResult AddAssetModel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAssetModel(AddAssetModelViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var category = db.FindOrAddAssetModelCategory(vm.Category);
                var properties = new List<AssetModelProperty>();
                foreach(var property in vm.Property)
                {
                    properties.Add(db.FindOrAddAssetModelProperty(property));
                }

                var model = new AssetModel()
                {
                    Name = vm.Name,
                    AssetModelCategory = category,
                    Properties = properties
                };

                db.AssetModels.Add(model);
                db.SaveChanges();
            }

            return View();
        }

        // GET: AddAsset
        public ActionResult AddAsset()
        {
            return View(new AddAssetViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAsset(AddAssetViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var asset = new Asset()
                {
                    Name = vm.Name,
                    SerialNumber = vm.SerialNumber,
                };

                var location = new AssetLocation()
                {
                    Asset = asset,
                    TimeFrom = DateTime.Now,
                    TimeTo = null,
                    Room = db.Rooms.Find(vm.SelectedRoomId),
                };

                asset.Locations = new List<AssetLocation>()
                {
                    location,
                };

                var properties = new List<AssetProperty>();
                foreach(var vmProp in vm.Properties)
                {
                    var property = new AssetProperty()
                    {
                        Asset = asset,
                        AssetModelProperty = db.AssetModelProperties.Find(vmProp.ModelId),
                        Value = vmProp.Value,
                    };

                    properties.Add(property);
                }

                asset.AssetProperties = properties;

                db.AssetLocations.Add(location);
                db.AssetProperties.AddRange(properties);
                db.Assets.Add(asset);
                db.SaveChanges();
            }
            else
            {
                return View(vm);
            }

            return View(vm);
        }

        public JsonResult GetAssetModelProperties(int id)
        {
            var properties = (from m in db.AssetModels where m.Id == id select m).First().Properties;
            var propertiesList = (from p in properties orderby p.Id select new { p.Id, p.Name }).ToArray();
            return Json(propertiesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCategory(int? id = 1)
        {
            return View(new ViewCategoryViewModel(id.Value));
        }

        public ActionResult ViewAssetModel(int? id = 1)
        {
            return View(new ViewAssetModelViewModel(id.Value));
        }

        public ActionResult ViewAsset(int? id = 1)
        {
            return View();
        }
    }
}