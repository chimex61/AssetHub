using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Account.Partial
{
    public class UserReportFormViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Date from")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Date to")]
        public DateTime DateTo { get; set; }
    }
}