using AssetHub.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetHub.Models
{
    public class User : IdentityUser
    {
        public class Validator
        {
            public const string FIRST_NAME_REQUIRED = "First name is required";

            public const string LAST_NAME_REQUIRED = "Last name is required";

            public const string POSITION_REQUIRED = "Position is required";

            public const string ROOM_REQUIRED = "Room is required";

            public const string EMAIL_REQUIRED = "Email is required";

            public const string INVALID_EMAIL = "Invalid email";

            public const string DUPLICATE_EMAIL = "User with this email already exists";

            public const string USERNAME_REQUIRED = "Username is required";

            public const string PASSWORD_REQUIRED = "Password is required";

            public const string PASSWORD_REGEX = @"^.{6,}";

            public const string INVALID_PASSWORD = "Password length must be at least 6 characters.";

            public const string PASSWORD_DIFFERENT = "Passwords don't match";

            public static string ValidateFirstName(string name)
            {
                return string.IsNullOrWhiteSpace(name) ? FIRST_NAME_REQUIRED : null;
            }

            public static string ValidateLastName(string name)
            {
                return string.IsNullOrWhiteSpace(name) ? LAST_NAME_REQUIRED : null;
            }

            public static string ValidatePosition(int positionId)
            {
                return positionId < 0 ? POSITION_REQUIRED : null;
            }

            public static string ValidateRoom(int roomId)
            {
                return roomId < 0 ? ROOM_REQUIRED : null;
            }

            public static string ValidateEmail(string id, string email)
            {
                if(string.IsNullOrWhiteSpace(email)) { return EMAIL_REQUIRED; }

                try
                {
                    var mail = new System.Net.Mail.MailAddress(email);

                    using (var db = new AssetHubContext())
                    {
                        var existing = (from u in db.Users
                                        where u.Email.ToLower().Contains(email.ToLower()) && u.Id != id
                                        select u).Count();

                        return existing > 0 ? DUPLICATE_EMAIL : null;
                    }
                }
                catch
                {
                    return INVALID_EMAIL;
                }
            }
        }

        public const string SAVE_SUCCESS = "User is saved successfully";

        public const string SAVE_FAIL = "User save failed";

        public const string DELETE_SUCCESS = "User is deleted successfully";

        public const string DELETE_FAIL = "User delete failed";

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public int? UserPositionId { get; set; }

        public int? RoomId { get; set; }

        public virtual UserPosition UserPosition { get; set; }

        public virtual Room Room { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}