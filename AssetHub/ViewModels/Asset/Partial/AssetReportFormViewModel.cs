using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Asset.Partial
{
    public class AssetReportFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date from")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Date to")]
        public DateTime DateTo { get; set; }
    }
}