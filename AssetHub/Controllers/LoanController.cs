using AssetHub.DAL;
using AssetHub.ViewModels.Loan.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.Controllers
{
    public class LoanController : Controller
    {
        AssetHubContext db = new AssetHubContext();

        //GET: CreateLoan
        public ActionResult CreateLoan(int id)
        {
            return View(new CreateLoanViewModel
            {
                AssetId = id,
                AssetName = db.Assets.Find(id).Name,
                Rooms = db.RoomDropdown(),
                Hours = GenerateHours(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLoan(CreateLoanViewModel vm)
        {
            return View(vm);
        }

        private IEnumerable<SelectListItem> GenerateHours()
        {
            var list = new List<SelectListItem>();
            for(var i = 0; i < 24; i++)
            {
                list.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                });
            }

            return new SelectList(list, "Value", "Text");
        }
    }
}