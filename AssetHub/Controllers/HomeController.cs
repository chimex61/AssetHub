using AssetHub.DAL;
using AssetHub.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var currentUser = SessionExtension.Account(Session);
            return View(new IndexViewModel(currentUser.Id));
        }
    }
}