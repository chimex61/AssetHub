using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetHub.ViewModels.Account.Partial
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = User.Validator.USERNAME_REQUIRED)]
        public string Username { get; set; }

        [Display(Name = "Old password")]
        [Required(ErrorMessage = User.Validator.PASSWORD_REQUIRED)]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [RegularExpression(User.Validator.PASSWORD_REGEX, ErrorMessage = User.Validator.INVALID_PASSWORD)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm passwod")]
        [RegularExpression(User.Validator.PASSWORD_REGEX, ErrorMessage = User.Validator.INVALID_PASSWORD)]
        public string ConfirmPassword { get; set; }
    }
}