using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Room
{
    public class ViewRoomViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public ViewRoomViewModel()
        {
            CurrentUsers = new List<Models.User>();
            CurrentAssets = new List<Models.Asset>();
        }

        public ViewRoomViewModel(int id) : this()
        {
            var room = db.Rooms.Find(id);

            Name = room.Name;

            CurrentAssets = (from l in db.AssetLocations
                             join a in db.Assets on l.AssetId equals a.Id
                             where DateTime.Now >= l.TimeFrom && (DateTime.Now <= l.TimeTo || l.TimeTo == null) && l.RoomId == id
                             select a).ToList();

            CurrentUsers = (from u in db.Users
                            where u.RoomId == id
                            select u).ToList();
        }

        public string Name { get; set; }

        public List<Models.User> CurrentUsers { get; set; }

        public List<Models.Asset> CurrentAssets { get; set; }
    }
}