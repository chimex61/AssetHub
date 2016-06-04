using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Home
{
    public class IndexViewModel
    {
        AssetHubContext db = new AssetHubContext();
        public IndexViewModel(string userId)
        {
            CurrentLoans = (from l in db.Loans
                            where l.User.Id == userId
                            && DateTime.Now >= l.TimeFrom && DateTime.Now <= l.TimeTo
                            select l).ToList();
        }

        public ICollection<Models.Loan> CurrentLoans { get; set; }
    }
}