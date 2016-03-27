using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssetHub.DAL
{
    public class AssetHubContextInitializer : DropCreateDatabaseAlways<AssetHubContext>
    {
        protected override void Seed(AssetHubContext context)
        {
            PreformInitialSetup(context);
            base.Seed(context);
        }

        public void PreformInitialSetup(AssetHubContext context)
        {
            // Create admin account....
        }
    }
}