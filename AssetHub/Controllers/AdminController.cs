using AssetHub.DAL;
using AssetHub.Infrastructure;
using AssetHub.Models;
using AssetHub.ViewModels.Admin;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;

namespace AssetHub.Controllers
{
    public class AdminController : Controller
    {
        private AssetHubContext db = new AssetHubContext();

        private AssetHubUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AssetHubUserManager>(); }
        }

        // Get: CreateUser
        public ActionResult CreateUser()
        {
            var positions = db.UserPositionList();
            var rooms = db.RoomList();
            return Json(positions, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(CreateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var room = db.FindOrAddRoom(vm.Room);
                var position = db.FindOrAddUserPosition(vm.Position);

                var user = new User
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserName = vm.Email,
                    Email = vm.Email,
                    UserPosition = position,
                    Room = room
                };

                var result = await UserManager.CreateAsync(user, vm.Password);

                if(result.Succeeded)
                {
                    ModelState.AddModelError("", "Unable to create user");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "not ok");
                return View(vm);
            }
            return View(vm);
        }
    }
}