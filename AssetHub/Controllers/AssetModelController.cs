using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.AssetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class AssetModelController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        public ActionResult Index()
        {
            return View(new IndexViewModel
            {
                Categories = db.CategoryDropdown(),
                AssetModels = db.AssetModels.ToList(),
            });
        }

        // GET: AddAssetModel
        public ActionResult AddAssetModel()
        {
            return View(new AddAssetModelViewModel
            {
                Categories = db.CategoryDropdown(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAssetModel(AddAssetModelViewModel vm)
        {
            var success = false;
            var message = "";

            var category = db.AssetModelCategories.Find(vm.SelectedCategoryId);
            var model = (from m in category.AssetModels
                         where m.Name.Equals(vm.Name, StringComparison.CurrentCultureIgnoreCase)
                         select m).FirstOrDefault();

            var sb = new StringBuilder();
            if (model != null)
            {
                ModelState.AddModelError("Name", AssetModel.NAME_EXISTS);
                sb.AppendLine("Name: " + AssetModel.NAME_EXISTS);
            }
            if(vm.Properties != null)
            {
                foreach(var p in vm.Properties)
                {
                    if(string.IsNullOrEmpty(p.Name))
                    {
                        sb.AppendLine("Properties: " + AssetModel.PROPERTY_NAME_REQUIRED);
                        ModelState.AddModelError("Properties", AssetModel.PROPERTY_NAME_REQUIRED);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var newModel = new AssetModel
                    {
                        AssetModelCategory = category,
                        Name = vm.Name,
                        Properties = new List<AssetModelProperty>(),
                    };
                    if(vm.Properties != null)
                    {
                        foreach(var p in vm.Properties)
                        {
                            newModel.Properties.Add(new AssetModelProperty
                            {
                                AssetModel = newModel,
                                Name = p.Name,
                                Expression = p.Expression,
                            });
                        }
                    }
                    db.AssetModels.Add(newModel);
                    db.SaveChanges();

                    success = true;
                    message = AssetModel.SAVE_SUCCESS;
                }
                catch (Exception e)
                {
                    message = AssetModel.SAVE_FAIL;
                }
            }
            return Json(new { Success = success, Message = message, Errors = sb.ToString() });
        }

        // GET: ViewAssetModel
        public ActionResult ViewAssetModel(int? id = 1)
        {
            return View(new ViewAssetModelViewModel(id.Value));
        }

        public JsonResult GetProperties()
        {
            var properties = (from p in db.AssetModelProperties
                              select new
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Expression = "\n",
                              });

            return Json(new { Properties = properties }, JsonRequestBehavior.AllowGet);
        }
    }
}