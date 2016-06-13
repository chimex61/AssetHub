using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Admin.Partial
{
    public class UserPositionManagementViewModel
    {
        public class PositionEditor
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        AssetHubContext db = new AssetHubContext();

        public UserPositionManagementViewModel()
        {
            DeletedPositions = new List<PositionEditor>();
            Positions = (from r in db.UserPositions
                     select new PositionEditor
                     {
                         Id = r.Id,
                         Name = r.Name,
                     }).ToList();
            NewPositions = new List<PositionEditor>();
        }

        public List<PositionEditor> DeletedPositions { get; set; }

        public List<PositionEditor> Positions { get; set; }

        public List<PositionEditor> NewPositions { get; set; }
    }
}