using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetHub.Models
{
    public partial class Asset
    {
        public class Validator
        {
            public const string NAME_REQUIRED = "Asset name is required";

            public const string SERIAL_REQUIRED = "Serial number is required";

            public const string MODEL_REQUIRED = "Model for asset is required";

            public const string PROPERTY_VALUE_REQUIRED = "Property value is required";

            public const string PROPERTY_VALUE_INVALID = "Property value is invalid";

            public static string ValidateAssetModel(int id)
            {
                return id < 0 ? MODEL_REQUIRED : null;
            }

            public static string ValidateName(string name)
            {
                return string.IsNullOrWhiteSpace(name) ? NAME_REQUIRED : null;
            }

            public static string ValidateSerialKey(string key)
            {
                return string.IsNullOrWhiteSpace(key) ? SERIAL_REQUIRED : null;
            }

            public static string ValidatePropertyValue(AssetModelProperty property, string value)
            {
                if(string.IsNullOrWhiteSpace(value)) { return PROPERTY_VALUE_REQUIRED; }

                if(property.IsNumeric)
                {
                    double result;
                    if(!double.TryParse(value, out result)) { return PROPERTY_VALUE_INVALID; }
                }

                return null;
            }
        }

        public const string SAVE_SUCCESS = "Asset is saved successfully";

        public const string SAVE_FAIL = "Asset save failed";

        public const string DELETE_SUCCESS = "Asset is deleted successfully";

        public const string DELETE_FAIL = "Asset delete failed";

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Value { get; set; }

        public string SerialNumber { get; set; }

        public int AssetModelId { get; set; }

        public virtual AssetModel AssetModel { get; set; }

        public virtual ICollection<AssetProperty> AssetProperties { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }

        public virtual ICollection<AssetLocation> Locations { get; set; }
    }
}