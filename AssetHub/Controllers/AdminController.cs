using AssetHub.DAL;
using AssetHub.Infrastructure;
using AssetHub.Models;
using AssetHub.ViewModels.AdminVMs;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace AssetHub.Controllers
{
    public class AdminController : Controller
    {
        private AssetHubContext db = new AssetHubContext();

        private AssetHubUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AssetHubUserManager>(); }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Get: CreateUser
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(CreateUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var room = (from r in db.Rooms
                            where r.Name.Equals(vm.Room, StringComparison.InvariantCultureIgnoreCase)
                            select r).FirstOrDefault();

                if (room == null)
                {
                    room = new Room { Name = vm.Room };
                    db.Rooms.Add(room);
                }

                var position = (from p in db.Positions
                                where p.Name.Equals(vm.Position, StringComparison.InvariantCultureIgnoreCase)
                                select p).FirstOrDefault();

                if (position == null)
                {
                    position = new Position { Name = vm.Position };
                    db.Positions.Add(position);
                }

                var user = new UserAccount
                {
                    Name = vm.Name,
                    UserName = vm.Email,
                    Email = vm.Email,
                    Position = position,
                    Room = room
                };

                var result = await UserManager.CreateAsync(user, vm.Email);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(vm);
        }


    }
}