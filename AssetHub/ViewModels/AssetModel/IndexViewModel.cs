using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.AssetModel
{
    public class IndexViewModel
    {
        public List<Models.AssetModel> AssetModels { get; set; }
    }
}