using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset
    {
        public class Validator
        {
            public const string NAME_REQUIRED = "Model name is required";

            public const string SERIAL_REQUIRED = "Serial key is required";

            public const string MODEL_REQUIRED = "Model for asset is required";

            public const string PROPERTY_VALUE_REQUIRED = "Property value is required";

            public const string PROPERTY_VALUE_INVALID = "Property value is invalid";
        }

        public const string SAVE_SUCCESS = "Asset is saved successfully";

        public const string SAVE_FAIL = "Asset save failed";

        public const string DELETE_SUCCESS = "Asset is deleted successfully";

        public const string DELETE_FAIL = "Asset delete failed";

        public int Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }

        public virtual ICollection<AssetProperty> AssetProperties { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }

        public virtual ICollection<AssetLocation> Locations { get; set; }
    }
}