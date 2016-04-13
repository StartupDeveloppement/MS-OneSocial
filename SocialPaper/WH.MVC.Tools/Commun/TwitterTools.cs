using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using WH.MVC.Tools.Twitter;

namespace WH.MVC.Tools.Commun
{
    public class TwitterTools
    {
        public static string GetTwitterConsumerKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConsumerKey"];
        }

        public static string GetTwitterConsumerSecret()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ConsumerSecret"];
        }

        public static string GetTwitterAuthorizeUrl(string pCallBackUrl)
        {
            TwitterAuthTokenResponse reqToken =  GetRequestToken(
                                TwitterTools.GetTwitterConsumerKey(),
                                TwitterTools.GetTwitterConsumerSecret(),
                                pCallBackUrl);

            return string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", reqToken.Token);
        }

        public static TwitterAuthTokens GetTwitterAccesTokens(string pRequestToken, string pPin)
        {
            string oauth_consumer_key = TwitterTools.GetTwitterConsumerKey();
            string oauth_consumer_secret = TwitterTools.GetTwitterConsumerSecret();

            TwitterAuthTokenResponse tokens = TwitterTools.GetAccessToken(
                        oauth_consumer_key,
                        oauth_consumer_secret,
                        pRequestToken,
                        pPin);

            TwitterAuthTokens accessTokens = new TwitterAuthTokens()
            {
                AccessToken = tokens.Token,
                AccessTokenSecret = tokens.TokenSecret,
                ConsumerKey = oauth_consumer_key,
                ConsumerSecret = oauth_consumer_secret
            };

            return accessTokens;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="requestToken">The request token.</param>
        /// <param name="verifier">The pin number or verifier string.</param>
        /// <returns>
        /// An <see cref="OAuthTokenResponse"/> class containing access token information.
        /// </returns>
        public static TwitterAuthTokenResponse GetAccessToken(string consumerKey, string consumerSecret, string requestToken, string verifier)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }

            if (string.IsNullOrEmpty(requestToken))
            {
                throw new ArgumentNullException("requestToken");
            }

            WebRequestBuilder builder = new WebRequestBuilder(
                new Uri("https://api.twitter.com/oauth/access_token"),
                HTTPVerb.GET,
                new TwitterAuthTokens { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret });

            if (!string.IsNullOrEmpty(verifier))
            {
                builder.Parameters.Add("oauth_verifier", verifier);
            }

            builder.Parameters.Add("oauth_token", requestToken);

            string responseBody;

            HttpWebResponse webResponse = builder.ExecuteRequest();

            responseBody = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            
            TwitterAuthTokenResponse response = new TwitterAuthTokenResponse();
            response.Token = Regex.Match(responseBody, @"oauth_token=([^&]+)").Groups[1].Value;
            response.TokenSecret = Regex.Match(responseBody, @"oauth_token_secret=([^&]+)").Groups[1].Value;
            response.UserId = long.Parse(Regex.Match(responseBody, @"user_id=([^&]+)").Groups[1].Value, CultureInfo.CurrentCulture);
            response.ScreenName = Regex.Match(responseBody, @"screen_name=([^&]+)").Groups[1].Value;
            return response;
        }

        /// <summary>
        /// Gets the request token.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        /// <param name="callbackAddress">The callback address. For PIN-based authentication "oob" should be supplied.</param>
        /// <returns></returns>
        public static TwitterAuthTokenResponse GetRequestToken(string consumerKey, string consumerSecret, string callbackAddress)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            if (string.IsNullOrEmpty(consumerSecret))
            {
                throw new ArgumentNullException("consumerSecret");
            }

            if (string.IsNullOrEmpty(callbackAddress))
            {
                throw new ArgumentNullException("callbackAddress", @"You must always provide a callback url when obtaining a request token. For PIN-based authentication, use ""oob"" as the callback url.");
            }

            WebRequestBuilder builder = new WebRequestBuilder(
                new Uri("https://api.twitter.com/oauth/request_token"),
                HTTPVerb.POST,
                new TwitterAuthTokens { ConsumerKey = consumerKey, ConsumerSecret = consumerSecret });

            if (!string.IsNullOrEmpty(callbackAddress))
            {
                builder.Parameters.Add("oauth_callback", callbackAddress);
            }

            string responseBody = null;

            HttpWebResponse webResponse = builder.ExecuteRequest();
            Stream responseStream = webResponse.GetResponseStream();
            if (responseStream != null) responseBody = new StreamReader(responseStream).ReadToEnd();

            return new TwitterAuthTokenResponse
            {
                Token = ParseQuerystringParameter("oauth_token", responseBody),
                TokenSecret = ParseQuerystringParameter("oauth_token_secret", responseBody),
                VerificationString = ParseQuerystringParameter("oauth_verifier", responseBody)
            };
        }

        /// <summary>
        /// Tries to the parse querystring parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="text">The text.</param>
        /// <returns>The value of the parameter or an empty string.</returns>
        /// <remarks></remarks>
        private static string ParseQuerystringParameter(string parameterName, string text)
        {
            Match expressionMatch = Regex.Match(text, string.Format(@"{0}=(?<value>[^&]+)", parameterName));

            if (!expressionMatch.Success)
            {
                return string.Empty;
            }

            return expressionMatch.Groups["value"].Value;
        }

        public static HttpWebResponse Update(TwitterAuthTokens pTokens, string pMessage)
        {
            Uri uri = new Uri("http://api.twitter.com/1/statuses/update.json");
            HTTPVerb verb = HTTPVerb.POST;

            WebRequestBuilder requestBuilder = new WebRequestBuilder(uri, verb, pTokens);

            requestBuilder.Parameters.Add("status", pMessage);

            return requestBuilder.ExecuteRequest();
        }
    }
}
