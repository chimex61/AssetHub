using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Room.Partial
{
    public class RoomManagementViewModel
    {
        public class RoomEditor
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
        }

        AssetHubContext db = new AssetHubContext();

        public RoomManagementViewModel()
        {
            DeletedRooms = new List<RoomEditor>();
            Rooms = (from r in db.Rooms
                     select new RoomEditor
                     {
                         Id = r.Id,
                         Name = r.Name,
                     }).ToList();
            NewRooms = new List<RoomEditor>();
        }

        public List<RoomEditor> DeletedRooms { get; set; }

        public List<RoomEditor> Rooms { get; set; }
        
        public List<RoomEditor> NewRooms { get; set; } 
    }
}