using AssetHub.DAL;
using AssetHub.ViewModels.Category;
using System;
using System.Collections.Generic;
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
            if (ModelState.IsValid)
            {
                var category = db.FindOrAddAssetModelCategory(vm.Name);
            }

            return View();
        }

        // GET: ViewCategory
        public ActionResult ViewCategory(int? id = 1)
        {
            return View(new ViewCategoryViewModel(id.Value));
        }
    }
}