using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.Asset;
using AssetHub.ViewModels.Asset.Partial;
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
            return View(new IndexViewModel());
        }

        // GET: AddAsset
        public ActionResult AddAsset()
        {
            return View(new AddAssetViewModel
            {
                AssetModels = db.AssetModelDropdown(),
                Rooms = db.RoomDropdown(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAsset(AddAssetViewModel vm)
        {
            var Success = false;
            var Message = "";

            var nameValidation = Asset.Validator.ValidateName(vm.Name);
            if(nameValidation != null)
            {
                ModelState.AddModelError("Name", nameValidation);
            }

            var modelValidation = Asset.Validator.ValidateAssetModel(vm.SelectedAssetModelId);
            if(modelValidation != null)
            {
                ModelState.AddModelError("AssetModel", modelValidation);
            }

            var serialValidation = Asset.Validator.ValidateSerialKey(vm.SerialNumber);
            if(serialValidation != null)
            {
                ModelState.AddModelError("SerialNumber", serialValidation);
            }

            if (vm.Properties != null)
            {
                for(var i = 0; i < vm.Properties.Count; i++)
                {
                    var p = vm.Properties[i];
                    var property = db.AssetModelProperties.Find(p.PropertyId);
                    var propertyValidation = Asset.Validator.ValidatePropertyValue(property, p.Value);
                    if (propertyValidation != null)
                    {
                        ModelState.AddModelError($"Properties[{i}]", propertyValidation);
                    }
                }
            }

            if (vm.SelectedRoomId < 0)
            {
                ModelState.AddModelError("SelectedRoomId", "Room is required");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var asset = new Asset
                    {
                        AssetModelId = vm.SelectedAssetModelId,
                        Name = vm.Name,
                        SerialNumber = vm.SerialNumber,
                        AssetProperties = new List<AssetProperty>(),
                        Locations = new List<AssetLocation>(),
                    };

                    if(vm.Properties != null)
                    {
                        foreach(var p in vm.Properties)
                        {
                            asset.AssetProperties.Add(new AssetProperty
                            {
                                Asset = asset,
                                AssetModelPropertyId = p.PropertyId,
                                Value = p.Value,
                            });
                        }
                    }

                    var room = db.Rooms.Find(vm.SelectedRoomId);
                    var location = new AssetLocation
                    {
                        Asset = asset,
                        Room = room,
                        TimeFrom = DateTime.Now,
                        TimeTo = null,
                    };

                    db.Assets.Add(asset);
                    db.AssetLocations.Add(location);
                    db.SaveChanges();

                    Success = true;
                    Message = Asset.SAVE_SUCCESS;
                }
                catch (Exception e)
                {
                    Message = Asset.SAVE_FAIL;
                }
                return Json(new { Success, Message });
            }

            vm.AssetModels = db.AssetModelDropdown();
            vm.Rooms = db.RoomDropdown();
            return PartialView("_AddAsset", vm);
        }

        public JsonResult GetAssetModelProperties(int id)
        {
            var properties = (from m in db.AssetModels where m.Id == id
                              select m).First().Properties;

            var propertiesList = (from p in properties
                                  orderby p.Id
                                  select new
                                  {
                                      PropertyId = p.Id,
                                      Name = p.Name,
                                      IsNumeric = p.IsNumeric,
                                  }).ToArray();

            return Json(propertiesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAsset(int? id = 1)
        {
            return View(new ViewAssetViewModel(id.Value));
        }
    }
}