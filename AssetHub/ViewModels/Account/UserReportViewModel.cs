using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Account
{
    public class UserReportViewModel
    {
        public UserReportViewModel(string id, DateTime timeFrom, DateTime timeTo)
        {
            TimeFrom = timeFrom;
            TimeTo = timeTo;

            using (var db = new AssetHubContext())
            {
                var user = db.Users.Find(id);

                Name = $"{user.FirstName} {user.LastName}";
                Position = user.UserPositionId.HasValue ? user.UserPosition.Name : "Unknown";
                Room = user.RoomId.HasValue ? user.Room.Name : "Unknown";

                Loans = new List<Tuple<string, string, DateTime, DateTime>>();

                foreach (var l in user.Loans)
                {
                    var x = l.TimeFrom >= TimeFrom && l.TimeFrom <= TimeTo;
                    var y = l.TimeTo >= TimeFrom && l.TimeTo <= TimeTo;

                    if (!(x || y)) { continue; }

                    Loans.Add(new Tuple<string, string, DateTime, DateTime>(l.Asset.Name, l.Asset.SerialNumber, l.TimeFrom, l.TimeTo));
                }
            }
        }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Room { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public List<Tuple<string, string, DateTime, DateTime>> Loans { get; set; }
    }
}