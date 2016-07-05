using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Loan
{
    public class ViewLoanViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public ViewLoanViewModel(int id)
        {
            var l = db.Loans.Find(id);
            Id = id;
            Asset = l.Asset;
            TimeFrom = l.TimeFrom;
            TimeTo = l.TimeTo;
            Room = l.Room;
        }

        public int Id { get; set; }

        public Models.Asset Asset { get; set; }

        [Display(Name = "Time from")]
        public DateTime TimeFrom { get; set; }

        [Display(Name = "Time to")]
        public DateTime TimeTo { get; set; }

        public Models.Room Room { get; set; }
    }
}