using System.Web;

namespace System.Web.Mvc
{
    public static class GooglePlusHelper
    {
        public static HtmlString GooglePlusButton(this HtmlHelper htmlHelper)
        {
            string result = "<div class=\"g-plus\" data-action=\"share\" data-annotation=\"bubble\" data-height=\"15\" href=\"www.walid.com\"></div>" +
                            "<script type=\"text/javascript\">" +
                            "    window.___gcfg = {lang: 'fr'};" +
                            "    (function() {" +
                            "    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;" +
                            "    po.src = 'https://apis.google.com/js/plusone.js';" +
                            "    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);" +
                            "    })();" +
                            "</script>";
            return new HtmlString(result);
        }
    }
}
