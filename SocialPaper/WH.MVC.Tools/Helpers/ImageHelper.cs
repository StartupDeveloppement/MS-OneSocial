using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class ImageHelper
    {
        public static HtmlString Image(this HtmlHelper helper, string url, string alt)
        {
            return new HtmlString(String.Format("<img src=\"{0}\" alt=\"{1}\" />", url, alt));
        }

        public static HtmlString Image(this HtmlHelper helper, string url, string alt, int width, int height)
        {
            return new HtmlString(String.Format("<img src=\"{0}\" alt=\"{1}\" width=\"{2}\" height=\"{3}\" />", url, alt, width.ToString(), height.ToString()));

        }
    }
}
