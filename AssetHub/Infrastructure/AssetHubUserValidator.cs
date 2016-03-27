using AssetHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace AssetHub.Infrastructure
{
    public class AssetHubUserValidator : UserValidator<UserAccount> 
    {
        public AssetHubUserValidator(AssetHubUserManager manager) : base(manager) { }

        public override async Task<IdentityResult> ValidateAsync(UserAccount user)
        {
            var result = await base.ValidateAsync(user);

            return result;
        }
    }
}