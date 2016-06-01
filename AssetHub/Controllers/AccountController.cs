using AssetHub.DAL;
using AssetHub.Infrastructure;
using AssetHub.ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
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