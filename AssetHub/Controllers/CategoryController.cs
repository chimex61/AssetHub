using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.Category;
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
            return View(new IndexViewModel { Categories = db.AssetModelCategories.ToList() });
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
            var success = false;
            var message = "";

            var existing = (from c in db.AssetModelCategories
                            where c.Name.Equals(vm.Name, StringComparison.InvariantCultureIgnoreCase)
                            select c).FirstOrDefault();
            if (existing != null)
            {
                ModelState.AddModelError("Name", AssetModelCategory.NAME_EXISTS);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var category = new AssetModelCategory { Name = vm.Name };
                    db.AssetModelCategories.Add(category);
                    db.SaveChanges();
                    success = true;
                    message = AssetModelCategory.SAVE_SUCCESS;
                }
                catch (Exception e)
                {
                    message = AssetModelCategory.SAVE_FAIL;
                }
            }

            return Json(new { Success = success, Message = message });
        }

        // GET: ViewCategory
        public ActionResult ViewCategory(int id)
        {
            return View(new ViewCategoryViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditCategory(ViewCategoryViewModel vm)
        {
            var success = false;
            var message = "";

            if (string.IsNullOrWhiteSpace(vm.Name))
            {
                ModelState.AddModelError("Name", AssetModelCategory.NAME_REQUIRED);
            }

            if (ModelState.IsValid)
            {
                db.AssetModelCategories.Find(vm.Id).Name = vm.Name;

                try
                {
                    db.SaveChanges();
                    success = true;
                    message = AssetModelCategory.SAVE_SUCCESS;

                }
                catch (Exception e)
                {
                    message = AssetModelCategory.SAVE_FAIL;
                }
            }

            return Json(new { Success = success, Message = message });
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            var success = false;
            var message = "";

            try
            {
                db.Entry(new AssetModelCategory { Id = id }).State = EntityState.Deleted;
                db.SaveChanges();
                success = true;
                message = AssetModelCategory.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                message = AssetModelCategory.DELETE_FAIL;
            }

            return Json(new { Success = success, Message = message });
        }
    }
}