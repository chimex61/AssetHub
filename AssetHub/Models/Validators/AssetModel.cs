﻿using AssetHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.Models
{
    public partial class AssetModelCategory
    {
        public const string SAVE_SUCCESS = "Category is saved successfully";

        public const string SAVE_FAIL = "Category save failed";

        public const string DELETE_SUCCESS = "Category deleted successfully";

        public const string DELETE_FAIL = "Category delete failed";

        public class Validator
        {
            public const string NAME_REQUIRED = "Category name is required";

            public const string NAME_EXISTS = "Category with that name already exists";

            public static string ValidateName(int? id, string name)
            {
                using (var db = new AssetHubContext())
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        return NAME_REQUIRED;
                    }
                    else
                    {
                        var existing = (from c in db.AssetModelCategories
                                        where c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && c.Id != id
                                        select c).ToList();

                        if (existing.Count != 0)
                        {
                            return NAME_EXISTS;
                        }
                    }
                }
                return null;
            }
        }
    }
}