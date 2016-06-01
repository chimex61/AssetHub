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
using AssetHub.ViewModels.Admin.Partial;

namespace AssetHub.Controllers
{
    public class AdminController : Controller
    {
        private AssetHubContext db = new AssetHubContext();

        private AssetHubUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AssetHubUserManager>(); }
        }


        public ActionResult UserIndex()
        {
            return View(new UserIndexViewModel
            {
                Users = db.Users.ToList(),
            });
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

        public JsonResult GetSearchResults(SearchViewModel vm)
        {
            var users = (from u in db.Users
                          select u);

            //if (vm.SelectedPositionId != -1)
            //{
            //    users = (from u in users
            //             where u.UserPositionId.HasValue && u.UserPositionId.Value == vm.SelectedPositionId
            //             select u);
            //}

            if(vm.SelectedRoomId != -1)
            {
                users = (from u in users
                         where u.RoomId.HasValue && u.RoomId.Value == vm.SelectedRoomId
                         select u);
            }

            if(!string.IsNullOrWhiteSpace(vm.Name))
            {
                users = (from u in users
                         where (u.FirstName + " " + u.LastName).ToLower().Contains(vm.Name.ToLower())
                         select u);
            }

            var result = (from u in users
                          select new
                          {
                              Id = u.Id,
                              Name = u.FirstName + " " + u.LastName,
                              Position = u.UserPositionId.HasValue ? u.UserPosition.Name : "Unknown",
                              Room = u.RoomId.HasValue ? u.Room.Name : "Unknown",
                          }).ToArray();

            return Json(new { Success = result.Length != 0, Users = result }, JsonRequestBehavior.AllowGet);
        }
    }
}