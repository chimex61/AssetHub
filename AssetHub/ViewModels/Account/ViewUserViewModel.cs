using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Account
{
    public class ViewUserViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public ViewUserViewModel()
        {
            AdministratorRights = false;
            SameAsCurrent = false;
            CurrentLoans = new List<Models.Loan>();
        }

        public ViewUserViewModel(string id, bool adminRights, bool sameAsCUrrent) : this()
        {
            AdministratorRights = adminRights;
            SameAsCurrent = sameAsCUrrent;
            Id = id;

            var user = db.Users.Find(id);
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsAdmin = user.IsAdmin;
            Room = user.Room.Name;
            UserPosition = user.UserPosition.Name;

            CurrentLoans = (from l in db.Loans
                           where DateTime.Now >= l.TimeFrom && DateTime.Now <= l.TimeTo && l.UserId == Id
                           orderby l.TimeFrom
                           select l).ToList();
        }

        public string Id { get; set; }

        public bool AdministratorRights { get; set; }

        public bool SameAsCurrent { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Position")]
        public string UserPosition { get; set; }

        public string Room { get; set; }

        public List<Models.Loan> CurrentLoans { get; set; }
    }
}