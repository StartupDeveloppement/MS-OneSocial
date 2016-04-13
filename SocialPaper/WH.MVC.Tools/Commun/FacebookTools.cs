using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WH.MVC.Tools.Commun
{
    public static class FacebookTools
    {
        public static string GenerateFacebookAccesToken()
        {
            FacebookClient fb = new FacebookClient();

            fb.AppId = FacebookTools.GetFacebookAppId();
            fb.AppSecret = FacebookTools.GetFacebookAppSecret();

            dynamic result = fb.Get("oauth/access_token", new
            {
                client_id = fb.AppId,
                client_secret = fb.AppSecret,
                grant_type = "client_credentials",
                scope = "publish_stream,manage_pages"
            });

            return result.access_token;
        }

        public static string GetFacebookAppId()
        {
            return System.Configuration.ConfigurationManager.AppSettings["FacebookAppId"];
        }

        public static string GetFacebookAppSecret()
        {
            return System.Configuration.ConfigurationManager.AppSettings["FacebookAppSecret"];
        }

        public static string GetFbAuthUrl(string pReturnUrl)
        {
            return string.Format(@"https://www.facebook.com/dialog/oauth/?client_id={0}&redirect_uri={1}&scope={2}&state={3}",
                                    FacebookTools.GetFacebookAppId(),
                                    pReturnUrl,
                                    "publish_stream",
                                    pReturnUrl);
        }
    }
}
