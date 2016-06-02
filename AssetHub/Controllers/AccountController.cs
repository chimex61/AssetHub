using AssetHub.DAL;
using AssetHub.Infrastructure;
using AssetHub.ViewModels.Account;
using AssetHub.ViewModels.Account.Partial;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private AssetHubUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AssetHubUserManager>(); }
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            var id = User.Identity.GetUserId();
            var user = new AssetHubContext().Users.Find(id);
            if(user != null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var acc = UserManager.FindAsync(vm.Email, vm.Password).Result;

                if (acc == null)
                {
                    ModelState.AddModelError("", "Invalid email or password!");
                    return View();
                }
                else
                {
                    var ident = UserManager.CreateIdentityAsync(acc, DefaultAuthenticationTypes.ApplicationCookie).Result;
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            var regex = new Regex(Models.User.Validator.PASSWORD_REGEX);

            if(string.IsNullOrEmpty(vm.Username))
            {
                ModelState.AddModelError("Username", Models.User.Validator.USERNAME_REQUIRED);
            }

            if (string.IsNullOrEmpty(vm.OldPassword)) 
            {
                ModelState.AddModelError("OldPassword", Models.User.Validator.PASSWORD_REQUIRED);
            }
            else if(!regex.IsMatch(vm.OldPassword))
            {
                ModelState.AddModelError("OldPassword", Models.User.Validator.INVALID_PASSWORD);
            }
            
            if(string.IsNullOrEmpty(vm.NewPassword))
            {
                ModelState.AddModelError("NewPassword", Models.User.Validator.PASSWORD_REQUIRED);
            }
            else if (!regex.IsMatch(vm.OldPassword))
            {
                ModelState.AddModelError("NewPassword", Models.User.Validator.INVALID_PASSWORD);
            }

            if (string.IsNullOrEmpty(vm.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", Models.User.Validator.PASSWORD_REQUIRED);
            }
            else if (!regex.IsMatch(vm.OldPassword))
            {
                ModelState.AddModelError("ConfirmPassword", Models.User.Validator.INVALID_PASSWORD);
            }

            if(vm.NewPassword != vm.ConfirmPassword)
            {
                ModelState.AddModelError("NewPassword", Models.User.Validator.PASSWORD_DIFFERENT);
                ModelState.AddModelError("ConfirmPassword", Models.User.Validator.PASSWORD_DIFFERENT);
            }

            var user = UserManager.Find(vm.Username, vm.OldPassword);
            if(user == null || user.Id != vm.Id)
            {
                ModelState.AddModelError("", "Invalid username or password");
            }

            if(ModelState.IsValid)
            {
                var result = UserManager.ChangePassword(vm.Id, vm.OldPassword, vm.NewPassword);
                if(result.Succeeded)
                {
                    return Json(new { Success = true, Message = "Password changed successfully" });
                }
                
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err);
                }
            }
            return PartialView("_ChangePassword", vm);
        }


        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        // GET: ViewUser
        public ActionResult ViewUser(string id)
        {
            var currentId = User.Identity.GetUserId();
            var user = new AssetHubContext().Users.Find(currentId);

            return View(new ViewUserViewModel(id, user.IsAdmin, id == currentId));
        }
    }
}