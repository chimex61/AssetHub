using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.AssetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class AssetModelController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        // GET: AddProperty
        public ActionResult AddProperty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProperty(AddPropertyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var property = db.FindOrAddAssetModelProperty(vm.Name);
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
            if (ModelState.IsValid)
            {
                var category = db.FindOrAddAssetModelCategory(vm.Category);
                var properties = new List<AssetModelProperty>();
                foreach (var property in vm.Property)
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

        // GET: ViewAssetModel
        public ActionResult ViewAssetModel(int? id = 1)
        {
            return View(new ViewAssetModelViewModel(id.Value));
        }
    }
}