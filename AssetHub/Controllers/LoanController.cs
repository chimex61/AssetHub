using AssetHub.DAL;
using AssetHub.Models;
using AssetHub.ViewModels.Loan.Partial;
using Microsoft.AspNet.Identity;
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
            if(vm.SelectedHourFromId < 0)
            {
                ModelState.AddModelError("TimeFrom", Loan.Validator.TIME_FROM_REQUIRED);
            }
            if(vm.SelectedHourToId < 0)
            {
                ModelState.AddModelError("TimeTo", Loan.Validator.TIME_TO_REQUIRED);
            }

            var timeFrom = vm.DateFrom.AddHours(vm.SelectedHourFromId);
            var timeTo = vm.DateTo.AddHours(vm.SelectedHourToId);

            var timeFromValidation = Loan.Validator.ValidateTimeTo(timeFrom);
            if(timeFromValidation != null)
            {
                ModelState.AddModelError("TimeFrom", timeFromValidation);
            }

            var timeToValidation = Loan.Validator.ValidateTimeTo(timeTo);
            if(timeToValidation != null)
            {
                ModelState.AddModelError("TimeTo", timeFromValidation);
            }

            var intervalValidation = Loan.Validator.ValidateTimeInterval(timeFrom, timeTo);
            if(intervalValidation != null)
            {
                ModelState.AddModelError("TimeFrom", intervalValidation);
                ModelState.AddModelError("TimeTo", intervalValidation);
            }

            var availableValidation = Loan.Validator.ValidateAssetAvailabilty(vm.AssetId, timeFrom, timeTo);
            if(availableValidation != null)
            {
                ModelState.AddModelError("", availableValidation);
            }

            var roomValidation = Loan.Validator.ValidateRoom(vm.SelectedRoomId);
            if(roomValidation != null)
            {
                ModelState.AddModelError("SelectedRoomId", roomValidation);
            }
            
            if(ModelState.IsValid)
            {
                try
                {
                    var loan = new Loan
                    {
                        AssetId = vm.AssetId,
                        IsReturned = false,
                        RoomId = vm.SelectedRoomId,
                        TimeFrom = timeFrom,
                        TimeTo = timeTo,
                        UserId = User.Identity.GetUserId(),
                    };
                    db.Loans.Add(loan);
                    db.SaveChanges();

                    return Json(new { Success = true, Message = Loan.SAVE_SUCCESS });
                }
                catch (Exception e)
                {
                    return Json(new { Success = false, Message = Loan.SAVE_FAIL });
                }
            }
            vm.Rooms = db.RoomDropdown();
            return PartialView("_CreateLoan", vm);
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