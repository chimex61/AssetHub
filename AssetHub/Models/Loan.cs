using AssetHub.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Loan
    {
        public class Validator
        {
            public const string TIME_FROM_REQUIRED = "Time from is required";

            public const string TIME_TO_REQUIRED = "Time to is required";

            public const string TIME_INTERVAL_MISMATCH = "Time from must be before time to";

            public const string TIME_FROM_BEFORE_NOW = "Time from must be in future";

            public const string TIME_TO_BEFORE_NOW = "Time to must be in future";

            public const string ROOM_REQUIRED = "Room is required";

            public const string LOAN_EXISTS = "Loan already exists in this time interval";

            public static string ValidateTimeFrom(DateTime timeFrom)
            {
                return timeFrom < DateTime.Now ? TIME_FROM_BEFORE_NOW : null;
            }

            public static string ValidateTimeTo(DateTime timeTo)
            {
                return timeTo < DateTime.Now ? TIME_TO_BEFORE_NOW : null;
            }

            public static string ValidateTimeInterval(DateTime from, DateTime to)
            {
                return to < from ? TIME_INTERVAL_MISMATCH : null;
            }

            public static string ValidateAssetAvailabilty(int assetId, DateTime timeFrom, DateTime timeTo)
            {
                using (var db = new AssetHubContext())
                {
                    var existing = (from l in db.Loans
                                    where l.AssetId == assetId && !l.IsReturned
                                    && (timeFrom >= l.TimeFrom && timeFrom <= l.TimeTo || timeTo >= l.TimeFrom && timeTo <= l.TimeTo)
                                    select l).Count();

                    return existing != 0 ? LOAN_EXISTS : null;
                }
            }

            public static string ValidateRoom(int roomId)
            {
                return roomId < 0 ? ROOM_REQUIRED : null;
            }
        }

        public const string SAVE_SUCCESS = "Loan is saved successfully";
         
        public const string SAVE_FAIL = "Loan save failed";

        public int Id { get; set; }

        public bool IsReturned { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int AssetId { get; set; }

        public int RoomId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual User User { get; set; }

        public virtual Room Room { get; set; }
    }
}