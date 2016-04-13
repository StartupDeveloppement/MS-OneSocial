using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WH.MVC.Tools.Helpers;

namespace WH.MVC.Tools.Attributs
{
    // Résumé :
    //     Représente un attribut utilisé pour définir l'Action en cours d'utilisation.
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CurrentActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext pFilterContext)
        {
            base.OnActionExecuting(pFilterContext);

            string _action = pFilterContext.ActionDescriptor.ActionName;
            string _controller = pFilterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            CookiesHelper.AddToCookie("CurrentAction", _action, false);
            CookiesHelper.AddToCookie("CurrentController", _controller, false);
        }
    }
}
