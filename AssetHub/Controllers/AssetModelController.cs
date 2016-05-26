using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.AssetModel;
using AssetHub.ViewModels.AssetModel.Partial;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var Success = false;
            var Message = "";

            var categoryValidation = AssetModel.Validator.ValidateCategory(vm.SelectedCategoryId);
            if(categoryValidation != null)
            {
                ModelState.AddModelError("Category", categoryValidation);
            }

            var nameValidation = AssetModel.Validator.ValidateName(null, vm.Name, vm.SelectedCategoryId);
            if(nameValidation != null)
            {
                ModelState.AddModelError("Name", nameValidation);
            }

            if(vm.Properties != null)
            {
                var hasPropertyErrors = false;
                for (var i = 0; i < vm.Properties.Count; i++)
                {
                    var propertyValidation = AssetModel.Validator.ValidateProperty(vm.Properties[i].Name);
                    if(propertyValidation != null)
                    {
                        ModelState.AddModelError($"Properties[{i}].Name", propertyValidation);
                        hasPropertyErrors = true;
                    }
                }

                if(!hasPropertyErrors)
                {
                    var properties = (from p in vm.Properties
                                      select new Tuple<string, bool>(p.Name, p.IsNumeric)).ToList();
                    var propertiesValidation = AssetModel.Validator.ValidateProperties(properties);
                    if(propertiesValidation != null)
                    {
                        ModelState.AddModelError("Properties", propertiesValidation);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newModel = new AssetModel
                    {
                        AssetModelCategoryId = vm.SelectedCategoryId,
                        Name = vm.Name,
                        Properties = new List<AssetModelProperty>(),
                    };
                    if(vm.Properties != null)
                    {
                        foreach(var p in vm.Properties)
                        {
                            newModel.Properties.Add(new AssetModelProperty
                            {
                                AssetModels = new List<AssetModel>() { newModel },
                                Name = p.Name,
                                IsNumeric = p.IsNumeric,
                            });
                        }
                    }
                    db.AssetModels.Add(newModel);
                    db.SaveChanges();

                    Success = true;
                    Message = AssetModel.SAVE_SUCCESS;
                }
                catch (Exception e)
                {
                    Message = AssetModel.SAVE_FAIL;
                }
                return Json(new { Success, Message });
            }
            vm.Categories = db.CategoryDropdown();
            return PartialView("_AddAssetModel", vm);
        }

        // GET: ViewAssetModel
        public ActionResult ViewAssetModel(int id)
        {
            return View(new ViewAssetModelViewModel(id));
        }

        [HttpPost]
        public JsonResult DeleteAssetModel(int id)
        {
            var Success = false;
            var Message = "";

            try
            {
                db.Entry(new AssetModel { Id = id }).State = EntityState.Deleted;
                db.SaveChanges();
                Success = true;
                Message = AssetModel.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                Message = AssetModel.DELETE_FAIL;
            }

            return Json(new { Success, Message });
        }

        public JsonResult GetSearchResults(SearchViewModel vm)
        {
            var models = (from m in db.AssetModels
                          select m);

            if(vm.SelectedCategoryId != -1)
            {
                models = (from m in models
                          where m.AssetModelCategoryId == vm.SelectedCategoryId
                          select m);
            }

            if(!string.IsNullOrWhiteSpace(vm.Name))
            {
                models = (from m in models
                          where m.Name.ToLower().Contains(vm.Name.ToLower())
                          select m);
            }

            var result = (from m in models
                          select new
                          {
                              Id = m.Id,
                              Name = m.Name,
                              Category = m.AssetModelCategory.Name,
                              AssetCount = m.Assets.Count,
                          }).ToArray();

            return Json(new { Success = result.Length != 0, Models = result }, JsonRequestBehavior.AllowGet);
        }
    }
}