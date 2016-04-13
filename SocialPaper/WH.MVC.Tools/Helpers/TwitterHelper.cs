using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Web.Mvc
{
    public static class TwitterHelper
    {

        public static HtmlString TweetButton(this HtmlHelper helper)
        {
            string result = "<a href=\"https://twitter.com/share\" class=\"twitter-share-button\" data-lang=\"fr\">Tweet</a>" +
                            "<script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = \"https://platform.twitter.com/widgets.js\"; fjs.parentNode.insertBefore(js, fjs); } }(document, \"script\", \"twitter-wjs\");</script>";

            return new HtmlString(result);
        }           
    }
}
