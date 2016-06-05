using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Asset
{
    public class AssetReportViewModel
    {
        AssetHubContext db = new AssetHubContext();

        public AssetReportViewModel(int id, DateTime timeFrom, DateTime timeTo)
        {
            var asset = db.Assets.Find(id);

            Name = asset.Name;
            SerialNumber = asset.SerialNumber;
            AssetModel = asset.AssetModel.Name;

            TimeFrom = timeFrom;
            TimeTo = timeTo;

            Properties = (from p in asset.AssetProperties
                          select new Tuple<string, string>(p.AssetModelProperty.Name, p.Value)).ToList();

            Loans = new List<Tuple<string, DateTime, DateTime>>();

            foreach(var l in asset.Loans)
            {
                var x = l.TimeFrom >= TimeFrom && l.TimeFrom <= TimeTo;
                var y = l.TimeTo >= TimeFrom && l.TimeTo <= TimeTo;

                if (!(x||y)) { continue; }

                Loans.Add(new Tuple<string, DateTime, DateTime>($"{l.User.FirstName} {l.User.LastName}", l.TimeFrom, l.TimeTo));
            }
        }

        public string Name { get; set; }

        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public string AssetModel { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public List<Tuple<string, string>> Properties { get; set; }

        public List<Tuple<string, DateTime, DateTime>> Loans { get; set; }
    }
}