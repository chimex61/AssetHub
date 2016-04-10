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

        public ActionResult AddAsset()
        {
            return View(new AddAssetViewModel { AssetModels = GetAssetModels() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAsset(AddAssetViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var assetModel = (from m in db.AssetModels where m.Id == vm.SelectedAssetModel select m).FirstOrDefault();
                var asset = new Asset
                {
                    Name = vm.Name,
                    SerialNumber = vm.SerialNumber,
                    AssetModel = assetModel,
                };

                var modelProperties = (from p in assetModel.Properties orderby p.Name select p).ToArray();
                var properties = new List<AssetProperty>();
                for(int i = 0; i < assetModel.Properties.Count; i++)
                {
                    properties.Add(new AssetProperty
                    {
                        Asset = asset,
                        AssetModelProperty = modelProperties[i],
                        Value = vm.PropertyValue[i],
                    });
                }

                asset.AssetProperties = properties;

                db.AssetProperties.AddRange(properties);
                db.Assets.Add(asset);
                db.SaveChanges();
            }
            else
            {
                return View();
            }

            return View();
        }

        private IEnumerable<SelectListItem> GetAssetModels()
        {
            var assetModels = db.AssetModels.Select(
                m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name,
                });

            return new SelectList(assetModels, "Value", "Text");
        }

        public JsonResult GetAssetModelProperties(int id)
        {
            var properties = (from m in db.AssetModels where m.Id == id select m).First().Properties;
            var propertiesList = (from p in properties orderby p.Name select p.Name).ToArray();
            return Json(propertiesList, JsonRequestBehavior.AllowGet);
        }
    }
}