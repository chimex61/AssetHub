using AssetHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetHub.DAL
{
    public static class SessionExtension
    {
        private const string ACCOUNT_KEY = "ACC_KEY";

        public static void Login(HttpSessionStateBase session, UserAccount account) => session[ACCOUNT_KEY] = account;

        public static void Logout(HttpSessionStateBase session) => session[ACCOUNT_KEY] = null;

        public static UserAccount Account(HttpSessionStateBase session) => (UserAccount)session[ACCOUNT_KEY];
    }
}