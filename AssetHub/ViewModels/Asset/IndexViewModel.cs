using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Asset
{
    public class IndexViewModel
    {
        public List<Models.Asset> Assets { get; set; }
    }
}