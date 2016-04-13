using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WH.MVC.Tools.Commun;

namespace System.Web.Mvc
{
    public static class FacebookHelper
    {
        public static HtmlString FbInitializeScript(this HtmlHelper helper)
        {
            string result = "<div id=\"fb-root\"></div>" +
                            "<script>(function(d, s, id) {" +
                                "var js, fjs = d.getElementsByTagName(s)[0];" +
                                "if (d.getElementById(id)) return;" +
                                "js = d.createElement(s); js.id = id;"+
                                "js.src = \"//connect.facebook.net/fr_FR/all.js#xfbml=1&appId=" + FacebookTools.GetFacebookAppId() + "\";"+
                                "fjs.parentNode.insertBefore(js, fjs);" +
                            "}(document, 'script', 'facebook-jssdk'));</script>";

            return new HtmlString(result);
        }

        public static HtmlString FbLikeButton(this HtmlHelper helper, string href)
        {
            string result = "<div class=\"fb-like\" data-href=\"" + href + "\" data-send=\"false\" data-width=\"450\" data-show-faces=\"false\" style=\"display:block\"></div>";
            result = FbInitializeScript(helper).ToHtmlString() + result;
            return new HtmlString(result);
        }

        public static HtmlString FbComments(this HtmlHelper helper, string href, bool initializeScript)
        {
            string result = "<div class=\"fb-comments\" data-href=\"" + href + "\" data-width=\"1280\" data-num-posts=\"10\" style=\"display:block\"></div>";
            if(initializeScript)
                result = FbInitializeScript(helper).ToHtmlString() + result;
            return new HtmlString(result);
        }
    }
}