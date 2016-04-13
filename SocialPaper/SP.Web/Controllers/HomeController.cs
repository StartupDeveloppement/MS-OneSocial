using SP.Model;
using SP.Web.Context;
using SP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LastArticles()
        {
            List<ArticleLink> articles = new List<ArticleLink>();
            foreach (Article a in ArticlesContext.GetLastPublishedArticles())
            {
                articles.Add(new ArticleLink { Id = a.Id, Title = a.Title, Site = UsersContext.GetSiteNameById(a.UserId) });
            }
            return PartialView("_LastArticlesPartial", articles);
        }

        [ChildActionOnly]
        public ActionResult PopularArticles()
        {
            List<ArticleLink> articles = new List<ArticleLink>();
            foreach (Article a in ArticlesContext.GetPopularArticles())
            {
                articles.Add(new ArticleLink { Id = a.Id, Title = a.Title, Site = UsersContext.GetSiteNameById(a.UserId) });
            }
            return PartialView("_PopularArticlesPartial", articles);
        }
    }
}
