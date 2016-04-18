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
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    SessionExtension.Login(Session, acc);
                }
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            AuthManager.SignOut();
            SessionExtension.Logout(Session);
            return RedirectToAction("Login", "Account");
        }
    }
}