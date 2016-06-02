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
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

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
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel vm)
        {
            var Success = false;
            var Message = "";

            var firstNameValidation = Models.User.Validator.ValidateFirstName(vm.FirstName);
            if(firstNameValidation != null)
            {
                ModelState.AddModelError("FirstName", firstNameValidation);
            }

            var lastNameValidation = Models.User.Validator.ValidateLastName(vm.LastName);
            if(lastNameValidation != null)
            {
                ModelState.AddModelError("LastName", lastNameValidation);
            }

            var emailValidation = Models.User.Validator.ValidateEmail(null, vm.Email);
            if(emailValidation != null)
            {
                ModelState.AddModelError("Email", emailValidation);
            }

            if(string.IsNullOrWhiteSpace(vm.Password))
            {
                ModelState.AddModelError("Password", Models.User.Validator.PASSWORD_REQUIRED);
            }
            else if(!(new Regex(Models.User.Validator.PASSWORD_REGEX).IsMatch(vm.Password)))
            {
                ModelState.AddModelError("Password", Models.User.Validator.INVALID_PASSWORD);
            }

            var positionValidation = Models.User.Validator.ValidatePosition(vm.SelectedPositionId);
            if(positionValidation != null)
            {
                ModelState.AddModelError("SelectedPositionId", positionValidation);
            }

            var roomValidation = Models.User.Validator.ValidateRoom(vm.SelectedRoomId);
            if(roomValidation != null)
            {
                ModelState.AddModelError("SelectedRoomId", roomValidation);
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserName = vm.Email,
                    IsAdmin = vm.IsAdministrator,
                    Email = vm.Email,
                    UserPositionId = vm.SelectedPositionId,
                    RoomId = vm.SelectedRoomId,
                };

                var result = UserManager.Create(user, vm.Password);

                if(result.Succeeded) { return Json(new { Success = true, Message = Models.User.SAVE_SUCCESS }); }

                foreach(var s in result.Errors)
                {
                    ModelState.AddModelError("", s);
                }

            }
            return PartialView("_CreateUser", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditUserViewModel vm)
        {
            var Success = false;
            var Message = "";

            var firstNameValidation = Models.User.Validator.ValidateFirstName(vm.FirstName);
            if(firstNameValidation != null)
            {
                ModelState.AddModelError("FirstName", firstNameValidation);
            }

            var lastNameValidation = Models.User.Validator.ValidateLastName(vm.LastName);
            if(lastNameValidation != null)
            {
                ModelState.AddModelError("LastName", lastNameValidation);
            }

            var emailValidation = Models.User.Validator.ValidateEmail(vm.Id, vm.Email);
            if(emailValidation != null)
            {
                ModelState.AddModelError("Email", emailValidation);
            }

            var positionValidation = Models.User.Validator.ValidatePosition(vm.SelectedPositionId);
            if(positionValidation != null)
            {
                ModelState.AddModelError("SelectedPositionId", positionValidation);
            }

            var roomValidation = Models.User.Validator.ValidateRoom(vm.SelectedRoomId);
            if(roomValidation != null)
            {
                ModelState.AddModelError("SelectedRoomId", roomValidation);
            }

            if (ModelState.IsValid)
            {
                var user = db.Users.Find(vm.Id);

                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = user.UserName = vm.Email;
                user.IsAdmin = vm.IsAdmin;
                user.RoomId = vm.SelectedRoomId;
                user.UserPositionId = vm.SelectedPositionId;

                try
                {
                    db.SaveChanges();
                    Success = true;
                    Message = Models.User.SAVE_SUCCESS;

                }
                catch (Exception)
                {
                    Message = Models.User.SAVE_FAIL;
                }

                return Json(new { Success, Message });
            }

            vm.Rooms = db.RoomDropdown();
            vm.Positions = db.UserPositionDropdown();
            return PartialView("_EditUser", vm);
        }

        public ActionResult DeleteUser(string id)
        {
            var Success = false;
            var Message = "";

            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                Success = true;
                Message = Models.User.DELETE_SUCCESS;
            }
            catch (Exception e)
            {
                Message = Models.User.DELETE_FAIL;
            }

            return Json(new { Success, Message });
        }

        // GET: UserPositionManagement
        public ActionResult UserPositionManagement()
        {
            return View(new UserPositionManagementViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserPositionManagement(UserPositionManagementViewModel vm)
        {
            var Success = false;
            var Message = "";

            if (vm.Positions != null)
            {
                for (var i = 0; i < vm.Positions.Count; i++)
                {
                    var positionValidation = UserPosition.Validator.ValidateName(vm.Positions[i].Name);
                    if (positionValidation != null)
                    {
                        ModelState.AddModelError($"Positions[{i}].Name", positionValidation);
                    }
                }
            }

            if (vm.NewPositions != null)
            {
                for (var i = 0; i < vm.NewPositions.Count; i++)
                {
                    var positionValidation = UserPosition.Validator.ValidateName(vm.NewPositions[i].Name);
                    if (positionValidation != null)
                    {
                        ModelState.AddModelError($"NewPositions[{i}].Name", positionValidation);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.DeletedPositions != null)
                    {
                        foreach (var r in vm.DeletedPositions)
                        {
                            db.Entry(new UserPosition { Id = r.Id }).State = EntityState.Deleted;
                        }
                    }

                    if (vm.Positions != null)
                    {
                        foreach (var r in vm.Positions)
                        {
                            var position = db.UserPositions.Find(r.Id);
                            position.Name = r.Name;
                        }
                    }

                    if (vm.NewPositions != null)
                    {
                        foreach (var r in vm.NewPositions)
                        {
                            db.UserPositions.Add(new UserPosition
                            {
                                Name = r.Name,
                            });
                        }
                    }

                    db.SaveChanges();

                    Success = true;
                    Message = UserPosition.SAVE_SUCCESS;
                }
                catch (Exception e)
                {
                    Message = UserPosition.SAVE_FAIL;
                }
                return Json(new { Success, Message });
            }
            return PartialView("_UserPositionManagement", vm);
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