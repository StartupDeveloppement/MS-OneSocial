using System;
using System.Web;

namespace WH.MVC.Tools.Helpers
{
    /// <summary>
    /// Classe de gestion des Actions : offre un ensemble de méthodes statiques simplifiant l'utilsiation des actions
    /// </summary>
    public static class ActionHelper
    {
        /// <summary>
        /// Renvoie le nom de l'action en cours : dernière action référencée.
        /// </summary>
        public static string GetCurrentAction
        {
            get
            {
                string _action = CookiesHelper.GetCookie("CurrentAction");

                if (String.IsNullOrEmpty(_action))
                    return "Index";
                else
                    return _action;
            }
        }

        /// <summary>
        /// Renvoie le nom du controlleur en cours : dernier controlleur référencé
        /// </summary>
        public static string GetCurrentController
        {
            get
            {
                string _controller = CookiesHelper.GetCookie("CurrentController");

                if (String.IsNullOrEmpty(_controller))
                    return "Articles";
                else
                    return _controller;
            }
        }
    }
}