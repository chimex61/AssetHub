using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetHub.ViewModels.Loan.Partial
{
    public class CreateLoanViewModel
    {
        public CreateLoanViewModel()
        {
            SelectedRoomId = -1;
            SelectedHourFromId = -1;
            SelectedHourToId = -1;
        }

        public int AssetId { get; set; }

        public string AssetName { get; set; }

        [Required]
        [Display(Name = "Time from")]
        public DateTime DateFrom { get; set; }

        public int SelectedHourFromId { get; set; }

        [Required]
        [Display(Name = "Time to")]
        public DateTime DateTo { get; set; }

        public int SelectedHourToId { get; set; }

        [Required(ErrorMessage = "Room is required")]
        [Display(Name = "Room")]
        public int SelectedRoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

        public IEnumerable<SelectListItem> Hours { get; set; }
    }
}