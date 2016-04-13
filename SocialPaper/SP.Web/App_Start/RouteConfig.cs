using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SP.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                name: "UserCV",
                url: "{sitename}/cv.htm",
                defaults: new { controller = "About", action = "CV" }
            );

            routes.MapRoute(
                name: "UserAbout",
                url: "{sitename}/apropos.htm",
                defaults: new { controller = "About", action = "Index" }
            );

            routes.MapRoute(
                name: "CreateLink",
                url: "associer-un-article.htm",
                defaults: new { sitename = (string)null, controller = "Article", action = "CreateLink" }
            );

            routes.MapRoute(
                name: "CreateArticle",
                url: "rediger-un-article.htm",
                defaults: new { sitename = (string)null, controller = "Article", action = "Create" }
            );

            routes.MapRoute(
                name: "DisplayArticleWithSiteName",
                url: "{sitename}/{id}/{titre}",
                defaults: new { controller = "Article", action = "Display" }
            );

            routes.MapRoute(
                name: "ListArticle",
                url: "{sitename}/articles.htm",
                defaults: new { controller = "Article", action = "List" }
            );

            routes.MapRoute(
                name: "ArticlesByCategory",
                url: "{sitename}/{category}.htm",
                defaults: new { controller = "Article", action = "Category" }
            );

            routes.MapRoute(
                name: "ManageArticles",
                url: "gerer-mes-articles.htm",
                defaults: new { sitename = (string)null, controller = "Article", action = "Manage" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "contact.htm",
                defaults: new { sitename = (string)null, controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { sitename = (string)null, controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Connected",
                url: "{sitename}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}