using AssetHub.DAL;
using AssetHub.ViewModels.Home;
using Microsoft.AspNet.Identity;
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
            var id = User.Identity.GetUserId();
            var user = new AssetHubContext().Users.Find(id);
            if (user == null) { return RedirectToAction("Login", "Account"); }

            return View(new IndexViewModel(user.Id));
        }
    }
}