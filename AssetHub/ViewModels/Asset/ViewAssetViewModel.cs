﻿using AssetHub.DAL;
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
        AssetHubContext db = new AssetHubContext();
        public ViewAssetViewModel(int id)
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
                           where l.TimeTo >= DateTime.Now
                           orderby l.TimeFrom
                           select l).ToList();

            CurrentLocation = (from l in db.AssetLocations
                               where l.AssetId == id && DateTime.Now >= l.TimeFrom && (DateTime.Now <= l.TimeTo || l.TimeTo == null) 
                               select l).FirstOrDefault();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset model")]
        public Models.AssetModel AssetModel { get; set; }

        public List<AssetProperty> Properties { get; set; }

        public List<Loan> PastLoans { get; set; }

        public Loan CurrentLoan { get; set; }

        public List<Loan> FutureLoans { get; set; }

        [Display(Name = "Current location")]
        public AssetLocation CurrentLocation { get; set; }
    }
}