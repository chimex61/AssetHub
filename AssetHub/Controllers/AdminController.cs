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
using AssetHub.ViewModels.Admin.Partial;
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

        // GET: RoomManagement
        public ActionResult RoomManagement()
        {
            return View(new RoomManagementViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomManagement(RoomManagementViewModel vm)
        {
            var Success = false;
            var Message = "";

            if (vm.Rooms != null)
            {
                for (var i = 0; i < vm.Rooms.Count; i++)
                {
                    var roomValidation = Room.Validator.ValidateName(vm.Rooms[i].Name);
                    if (roomValidation != null)
                    {
                        ModelState.AddModelError($"Rooms[{i}].Name", roomValidation);
                    }
                }
            }

            if (vm.NewRooms != null)
            {
                for (var i = 0; i < vm.NewRooms.Count; i++)
                {
                    var roomValidation = Room.Validator.ValidateName(vm.NewRooms[i].Name);
                    if (roomValidation != null)
                    {
                        ModelState.AddModelError($"NewRooms[{i}].Name", roomValidation);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(vm.DeletedRooms != null)
                    {
                        foreach(var r in vm.DeletedRooms)
                        {
                            db.Entry(new Room { Id = r.Id }).State = EntityState.Deleted;
                        }
                    }

                    if(vm.Rooms != null)
                    {
                        foreach(var r in vm.Rooms)
                        {
                            var room = db.Rooms.Find(r.Id);
                            room.Name = r.Name;
                        }
                    }

                    if(vm.NewRooms != null)
                    {
                        foreach(var r in vm.NewRooms)
                        {
                            db.Rooms.Add(new Room
                            {
                                Name = r.Name,
                            });
                        }
                    }

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
            return PartialView("_RoomManagement", vm);
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