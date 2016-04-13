using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using SP.Web.Models;
using WH.MVC.Tools.Commun;

namespace SP.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "000000004C0E234F",
                clientSecret: "lc3ZzFpQszTIWm8-qDAFQsFid5cY9yOG");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: TwitterTools.GetTwitterConsumerKey(),
                consumerSecret: TwitterTools.GetTwitterConsumerSecret());

            string fbAppId = FacebookTools.GetFacebookAppId();
            string fbAppSecret = FacebookTools.GetFacebookAppSecret();

            OAuthWebSecurity.RegisterFacebookClient(
                appId: fbAppId,
                appSecret: fbAppSecret);

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
