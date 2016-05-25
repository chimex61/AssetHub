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
            return View(vm);
        }

        public JsonResult GetAssetModelProperties(int id)
        {
            var properties = (from m in db.AssetModels where m.Id == id select m).First().Properties;
            var propertiesList = (from p in properties orderby p.Id select new { p.Id, p.Name }).ToArray();
            return Json(propertiesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAsset(int? id = 1)
        {
            return View(new ViewAssetViewModel(id.Value));
        }
    }
}