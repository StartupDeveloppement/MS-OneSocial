using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WH.MVC.Tools.Commun
{
    public static class HtmlTools
    {
        public static string HtmTitle(string pTitle)
        {
            StringBuilder sb = new StringBuilder(pTitle);
            sb.Replace(" ", "-");
            sb.Replace("é", "e");
            sb.Replace("ê", "e");
            sb.Replace("ê", "e");
            sb.Replace(":", "");
            sb.Replace("?", "");
            sb.Replace("à", "a");
            sb.Replace("â", "a");
            sb.Replace("ä", "a");
            sb.Replace("€", "");
            sb.Replace("&", "");
            sb.Replace("--", "-");
            sb.Append(".htm");

            return sb.ToString();
        }
    }
}
