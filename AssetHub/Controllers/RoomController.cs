using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.Room.Partial;
using AssetHub.ViewModels.Room;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class RoomController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        // GET: Room
        public ActionResult Index()
        {
            return View(new IndexViewModel
            {
                Rooms = db.Rooms.ToList(),
            });
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
                    if (vm.DeletedRooms != null)
                    {
                        foreach (var r in vm.DeletedRooms)
                        {
                            db.Entry(new Room { Id = r.Id }).State = EntityState.Deleted;
                        }
                    }

                    if (vm.Rooms != null)
                    {
                        foreach (var r in vm.Rooms)
                        {
                            var room = db.Rooms.Find(r.Id);
                            room.Name = r.Name;
                        }
                    }

                    if (vm.NewRooms != null)
                    {
                        foreach (var r in vm.NewRooms)
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

        public JsonResult GetSearchResults(SearchViewModel vm)
        {
            var rooms = (from c in db.Rooms
                              select c);

            if (!string.IsNullOrWhiteSpace(vm.Name))
            {
                rooms = (from c in rooms
                              where c.Name.ToLower().Contains(vm.Name.ToLower())
                              select c);
            }

            var result = (from c in rooms
                          select new
                          {
                              Id = c.Id,
                              Name = c.Name,
                          }).ToArray();

            return Json(new { Success = result.Length != 0, Rooms = result }, JsonRequestBehavior.AllowGet);
        }
    }
}