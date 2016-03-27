using AssetHub.DAL;
using AssetHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.Infrastructure
{
    public class AssetHubUserManager : UserManager<User>
    {
        public AssetHubUserManager(IUserStore<User> store) : base(store) { }

        public static AssetHubUserManager Create(IdentityFactoryOptions<AssetHubUserManager> options, IOwinContext context)
        {
            var db = context.Get<AssetHubContext>();
            var manager = new AssetHubUserManager(new UserStore<User>(db));

            manager.UserValidator = new AssetHubUserValidator(manager)
            {
                RequireUniqueEmail = true
            };

            return manager;
        }

        
    }
}