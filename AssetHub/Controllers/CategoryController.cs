using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.Category;
using AssetHub.ViewModels.Category.Partial;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class CategoryController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(new IndexViewModel
            {
                Categories = db.AssetModelCategories.ToList()
            });
        }

        // GET: AddCategory
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(AddCategoryViewModel vm)
        {
            var Success = false;
            var Message = "";

            var nameValidation = AssetModelCategory.Validator.ValidateName(null, vm.Name);
            if (nameValidation != null) { ModelState.AddModelError("Name", nameValidation); }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = new AssetModelCategory { Name = vm.Name };
                    db.AssetModelCategories.Add(category);
                    db.SaveChanges();
                    Success = true;
                    Message = AssetModelCategory.SAVE_SUCCESS;
                }
                catch (Exception)
                {
                    Message = AssetModelCategory.SAVE_FAIL;
                }
                return Json(new { Success, Message });
            }
            return PartialView("_AddCategory", vm);
        }

        // GET: ViewCategory
        public ActionResult ViewCategory(int id)
        {
            return View(new ViewCategoryViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(EditCategoryViewModel vm)
        {
            var Success = false;
            var Message = "";

            var nameValidation = AssetModelCategory.Validator.ValidateName(vm.Id, vm.Name);
            if(nameValidation != null) { ModelState.AddModelError("Name", nameValidation); }

            if (ModelState.IsValid)
            {
                db.AssetModelCategories.Find(vm.Id).Name = vm.Name;

                try
                {
                    db.SaveChanges();
                    Success = true;
                    Message = AssetModelCategory.SAVE_SUCCESS;

                }
                catch (Exception)
                {
                    Message = AssetModelCategory.SAVE_FAIL;
                }

                return Json(new { Success, Message });
            }
            
            return PartialView("_EditCategory", vm);
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            var Success = false;
            var Message = "";

            try
            {
                db.Entry(new AssetModelCategory { Id = id }).State = EntityState.Deleted;
                db.SaveChanges();
                Success = true;
                Message = AssetModelCategory.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                Message = AssetModelCategory.DELETE_FAIL;
            }

            return Json(new { Success, Message });
        }

        public JsonResult GetSearchResults(string name)
        {
            var categories = (from c in db.AssetModelCategories
                              where c.Name.ToLower().Contains(name.ToLower())
                              select new
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  ModelCount = c.AssetModels.Count
                              }).ToArray();

            return Json(new { Success = categories.Length != 0, Categories = categories }, JsonRequestBehavior.AllowGet);
        }
    }
}