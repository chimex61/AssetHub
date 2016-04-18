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
        public IndexViewModel(string userId)
        {
            using (var db = new AssetHubContext())
            {
                var currentTime = DateTime.Now;
                CurrentLoans = (from l in db.Loans
                                where l.User.Id == userId
                                && l.TimeFrom >= currentTime
                                && l.TimeFrom <= currentTime
                                select l).ToList();
            }
        }

        public ICollection<Loan> CurrentLoans { get; set; }
    }
}