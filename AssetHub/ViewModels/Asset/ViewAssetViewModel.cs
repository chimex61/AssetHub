using AssetHub.DAL;
using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Asset
{
    public class ViewAssetViewModel
    {
        public ViewAssetViewModel(int id)
        {
            using (var db = new AssetHubContext())
            {
                var asset = db.Assets.Find(id);

                Id = asset.Id;
                Name = asset.Name;
                SerialNumber = asset.SerialNumber;
                AssetModel = asset.AssetModel;
                Properties = asset.AssetProperties.ToList();

                PastLoans = (from l in db.Loans
                             where l.TimeTo <= DateTime.Now
                             orderby l.TimeFrom
                             select l).ToList();

                CurrentLoan = (from l in db.Loans
                               where l.TimeFrom >= DateTime.Now
                               orderby l.TimeFrom
                               select l).FirstOrDefault();

                FutureLoans = (from l in db.Loans
                             where l.TimeTo > DateTime.Now
                             orderby l.TimeFrom
                             select l).ToList();

                CurrentLocation = CurrentLoan != null ? CurrentLoan.AssetLocation : null;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public Models.AssetModel AssetModel { get; set; }

        public ICollection<AssetProperty> Properties { get; set; }

        public ICollection<Loan> PastLoans { get; set; }

        public Loan CurrentLoan { get; set; }

        public ICollection<Loan> FutureLoans { get; set; }

        public AssetLocation CurrentLocation { get; set; }
    }
}