using AssetHub.ViewModels.AssetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
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