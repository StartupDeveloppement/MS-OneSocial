using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WH.MVC.Tools.Commun
{
    public static class SecurityTools
    {
        /// <summary>
        /// Un workaround temporaire permettant d'éviter des injections de script dans le contenu HTML
        /// Il faudra s'inspirer de la librairie AntiXSS pour l'améliorer
        /// </summary>
        /// <param name="pInput">Le bloc HTML à sécuriser</param>
        /// <returns></returns>
        public static string GetHtmlWithoutScript(string pInput)
        {
            string output = pInput;

            while (output.IndexOf("<script") > 0)
            {
                int start = output.IndexOf("<script");
                int end = output.IndexOf("</script");
                if (end > start)
                {
                    string tmp = output.Substring(start, end - start + 9);
                    output = output.Replace(tmp, string.Empty);
                }
            }
            return output;
        }
    }
}
