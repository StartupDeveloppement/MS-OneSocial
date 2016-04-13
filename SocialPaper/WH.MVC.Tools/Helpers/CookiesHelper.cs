using System;
using System.Web;

namespace WH.MVC.Tools.Helpers
{
    /// <summary>
    /// Classe de gestion des cookies
    /// </summary>
    public class CookiesHelper
    {
        /// <summary>
        /// Permet d'ajouter une entrée dans les cookies
        /// </summary>
        /// <param name="pName">Nom de l'entrée</param>
        /// <param name="pValue">Valeur à stocker</param>
        /// <param name="pPersistant">Flag de persistance de la valeur</param>
        public static void AddToCookie(string pName, string pValue, bool pPersistant)
        {
            // Préparation du cookie
            HttpCookie _cookie = new HttpCookie(pName);
            _cookie.Value = pValue;
            // Si le cookie doit être persistant on spécifie la date d'expiration à une année
            if (pPersistant)
                _cookie.Expires = DateTime.Now.AddYears(1);

            // S'il n'existe pas on le crée
            if (HttpContext.Current.Request.Cookies[pName] == null)
            {
                System.Web.HttpContext.Current.Response.Cookies.Add(_cookie);
            }
            // Sinon on le modifie
            else
            {
                System.Web.HttpContext.Current.Response.Cookies[pName].Value = _cookie.Value;
                System.Web.HttpContext.Current.Response.Cookies[pName].Expires = _cookie.Expires;
            }
        }

        /// <summary>
        /// Renvoi la valeur d'un cookies à partir de son nom
        /// </summary>
        /// <param name="pName">Nom du cookie</param>
        /// <returns>Valeur du cookie s'il est trouvé si non une string vide</returns>
        public static string GetCookie(string pName)
        {
            if (null != HttpContext.Current.Request.Cookies[pName])
                return HttpContext.Current.Request.Cookies[pName].Value;
            else
                return String.Empty;
        }

        /// <summary>
        /// Supprime un cookie
        /// </summary>
        /// <param name="pName">Nom du cookie à supprimer</param>
        public static void DeleteCookie(string pName)
        {
            if (HttpContext.Current.Request.Cookies[pName] != null)
            {
                HttpCookie myCookie = new HttpCookie(pName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }
    }
}