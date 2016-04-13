using AutoMapper;
using DotNetOpenAuth.AspNet;
using Facebook;
using Microsoft.Web.WebPages.OAuth;
using SP.Model;
using SP.Web.Context;
using SP.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WH.MVC.Tools.Commun;
using WH.MVC.Tools.Twitter;

namespace SP.Web.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        #region - Category -
        [AllowAnonymous]
        public ActionResult Category(string sitename, string category)
        {
            ViewBag.Active = category;

            List<ArticleIndex> articles = new List<ArticleIndex>();
            if (string.IsNullOrEmpty(sitename) || string.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserProfile u = UsersContext.GetUserBySiteName(sitename);

                ViewBag.Title = "Articles " + category + " de " + u.UserName;
                ViewBag.UserName = u.UserName;
                ViewBag.SiteName = u.SiteName;
                ViewBag.Category = category;
                foreach (Article a in ArticlesContext.GetUserPublishedArticlesByCategory(u, category))
                {
                    ArticleIndex ai = Mapper.Map<Article, ArticleIndex>(a);
                    articles.Add(ai);
                }
            }
            return View(articles);
        }
        #endregion

        #region - List -
        [AllowAnonymous]
        public ActionResult List(string sitename)
        {
            ViewBag.Active = "List";

            List<ArticleIndex> articles = new List<ArticleIndex>();
            if (string.IsNullOrEmpty(sitename))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserProfile u = UsersContext.GetUserBySiteName(sitename);

                ViewBag.Title = "Articles de " + u.UserName;
                ViewBag.UserName = u.UserName;
                ViewBag.SiteName = u.SiteName;

                foreach (Article a in ArticlesContext.GetUserPublishedArticles(u, 0))
                {
                    ArticleIndex ai = Mapper.Map<Article, ArticleIndex>(a);
                    articles.Add(ai);
                }
            }
            return View(articles);
        }
        #endregion

        #region - Manage -
        public ActionResult Manage()
        {
            ViewBag.Active = "Manage";

            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
            ViewBag.UserName = u.UserName;
            ViewBag.SiteName = u.SiteName;

            List<ArticleIndex> articles = new List<ArticleIndex>();

            List<Article> ar = ArticlesContext.GetUserArticles(u);
            foreach (Article a in ar)
            {
                ArticleIndex ai = Mapper.Map<Article, ArticleIndex>(a);
                articles.Add(ai);
            }

            return View(articles);
        }
        #endregion

        #region - Display -
        [AllowAnonymous]
        public ActionResult Display(string sitename, int? id)
        {
            if (null != id || string.IsNullOrEmpty(sitename))
            {
                UserProfile u = UsersContext.GetUserBySiteName(sitename);

                if (null != u)
                {
                    ViewBag.UserName = u.UserName;
                    ViewBag.SiteName = u.SiteName;

                    Article article = ArticlesContext.GetPublishedArticleById(sitename, id.Value);
                    if (null != article)
                    {
                        if (!string.IsNullOrEmpty(article.Title))
                        {
                            ArticleContent viewModel = Mapper.Map<Article, ArticleContent>(article);
                            ViewBag.Title = article.Title;
                            ArticlesContext.IncrementeVisits(article);
                            return View(viewModel);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region - Edit -
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (null != id)
            {
                UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
                ViewBag.UserName = u.UserName;
                ViewBag.SiteName = u.SiteName;

                Article article = ArticlesContext.GetArticleById(id.Value);
                if (null != article)
                {
                    if (u.Id == article.UserId)
                    {
                        ArticleEdit viewModel = Mapper.Map<Article, ArticleEdit>(article);
                        return View(viewModel);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleEdit pArticle)
        {
            if (null != pArticle)
            {
                Article article = Mapper.Map<ArticleEdit, Article>(pArticle);

                ArticlesContext.UpdateArticle(article);
                return RedirectToAction("Manage", "Article");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditLink(int? id)
        {
            if (null != id)
            {
                UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
                ViewBag.UserName = u.UserName;
                ViewBag.SiteName = u.SiteName;

                Article article = ArticlesContext.GetArticleById(id.Value);
                if (null != article)
                {
                    if (u.Id == article.UserId)
                    {
                        ArticleEditLink viewModel = Mapper.Map<Article, ArticleEditLink>(article);
                        return View(viewModel);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditLink(ArticleEditLink pArticle)
        {
            if (null != pArticle)
            {
                Article article = Mapper.Map<ArticleEditLink, Article>(pArticle);

                ArticlesContext.UpdateArticle(article);
                return RedirectToAction("Manage", "Article");
            }
            return View();
        }
        #endregion

        #region - Publish -
        [HttpGet]
        public ActionResult Publish(int? id)
        {
            if (null != id)
            {
                UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
                ViewBag.UserName = u.UserName;
                ViewBag.SiteName = u.SiteName;

                Article article = ArticlesContext.GetArticleById(id.Value);
                if (null != article)
                {
                    if (u.Id == article.UserId)
                    {
                        bool isFacebook = false;
                        bool isGoogle = false;
                        bool isTwitter = false;

                        ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
                        List<ExternalLogin> externalLogins = new List<ExternalLogin>();
                        foreach (OAuthAccount account in accounts)
                        {
                            if (account.Provider.Equals("Facebook", System.StringComparison.InvariantCultureIgnoreCase))
                                isFacebook = true;
                            if (account.Provider.Equals("Google", System.StringComparison.InvariantCultureIgnoreCase))
                                isGoogle = true;
                            if (account.Provider.Equals("Twitter", System.StringComparison.InvariantCultureIgnoreCase))
                                isTwitter = true;
                        }
                        ArticlePublish articlePublish = new ArticlePublish();
                        articlePublish.isPublished = article.Publication > DateTime.MinValue;
                        articlePublish.isFbPublished = article.FbPublication > DateTime.MinValue;
                        articlePublish.isFacebook = isFacebook;
                        articlePublish.isGooglePublished = article.GooglePublication > DateTime.MinValue;
                        articlePublish.isGoogle = isGoogle;
                        articlePublish.isTwitterPublished = article.TwitterPublication > DateTime.MinValue;
                        articlePublish.isTwitter = isTwitter;

                        ArticlePublish viewModel = articlePublish;
                        viewModel.Content = new ArticleContent { Title = article.Title, Content = article.Content, File = article.File, Video = article.Video, LinkUrl = article.LinkUrl, isLink = article.isLink };
                        return View(viewModel);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Publish(ArticlePublish pArticle)
        {
            if (null != pArticle)
            {
                Article article = null;
                // Si l'article n'est pas déjà publié ou bien qu'on demande la republication
                if (!pArticle.isPublished || (pArticle.isPublished && pArticle.Publish))
                    article = ArticlesContext.PublishArticle(pArticle.Id);

                #region - Publication sur les réseaux sociaux -
                string fbProviderId = string.Empty;
                string googleProviderId = string.Empty;
                string twitterProviderId = string.Empty;
                string userSite = UsersContext.GetUserSite(User.Identity.Name);

                if (pArticle.PublishOnFacebook || pArticle.PublishOnGoogle || pArticle.PublishOnTwitter)
                {
                    ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
                    List<ExternalLogin> externalLogins = new List<ExternalLogin>();
                    foreach (OAuthAccount account in accounts)
                    {
                        if (account.Provider.Equals("Facebook", System.StringComparison.InvariantCultureIgnoreCase))
                            fbProviderId = account.ProviderUserId;
                        if (account.Provider.Equals("Google", System.StringComparison.InvariantCultureIgnoreCase))
                            googleProviderId = account.ProviderUserId;
                        if (account.Provider.Equals("Twitter", System.StringComparison.InvariantCultureIgnoreCase))
                            twitterProviderId = account.ProviderUserId;
                    }
                }
                if (article == null)
                    article = ArticlesContext.GetArticleById(pArticle.Id);

                string articleLink = Url.Action("Display", "Article", new { sitename = userSite, id = article.Id, titre = HtmlTools.HtmTitle(article.Title) });

                //string fullArticleLink = "http://" + this.Request.Url.Authority + "/" + userSite + "/" + article.Id + "/" + HtmlTools.HtmTitle(article.Title);
                string fullArticleLink = "" + articleLink;

                #region - Publication sur Facebook -
                if (pArticle.PublishOnFacebook)
                {
                    dynamic messagePost = new ExpandoObject();
                    if (!string.IsNullOrEmpty(article.Picture))
                    {
                        //messagePost.picture = "";
                        messagePost.picture = article.Picture;
                    }

                    messagePost.link = fullArticleLink;
                    messagePost.name = article.Title;

                    messagePost.caption = article.Title;
                    messagePost.description = article.Abstract;
                    //messagePost.message = article.Abstract;

                    string accessToken = FacebookTools.GenerateFacebookAccesToken();

                    FacebookClient fb = new FacebookClient(accessToken);

                    try
                    {
                        var postId = fb.Post(fbProviderId + "/feed", messagePost);
                        ArticlesContext.MarkFbPublication(pArticle.Id);
                        EventLog.WriteEntry("Application", "l'article a été publié sur facebook");
                    }
                    catch (FacebookOAuthException fbEx)
                    {
                        EventLog.WriteEntry("Application", fbEx.Message + fbEx.StackTrace, EventLogEntryType.Information);

                        return Redirect(FacebookTools.GetFbAuthUrl(this.Request.Url.ToString()));
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry("Application", ex.Message + "\n" + ex.StackTrace, EventLogEntryType.Error);
                    }
                }
                #endregion

                #region - Publication sur Twitter -
                if (pArticle.PublishOnTwitter)
                {
                    if (null == Session["TwitterAuthTokens"])
                    {
                        if (Request["oauth_token"] == null)
                        {
                            return Redirect(TwitterTools.GetTwitterAuthorizeUrl(Request.Url.AbsoluteUri));
                        }
                        else
                        {
                            TwitterAuthTokens tokens = TwitterTools.GetTwitterAccesTokens(Request["oauth_token"].ToString(), Request["oauth_verifier"].ToString());

                            Session["TwitterAuthTokens"] = tokens;
                        }
                    }

                    TwitterAuthTokens twitterTokens = Session["TwitterAuthTokens"] as TwitterAuthTokens;

                    string twitterMsg = article.Title + "\n" + fullArticleLink;

                    HttpWebResponse response = TwitterTools.Update(
                        twitterTokens,
                        twitterMsg);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ArticlesContext.MarkTwitterPublication(pArticle.Id);
                        EventLog.WriteEntry("Application", "Tweet réussi pour l'article : " + article.Title, EventLogEntryType.Information);
                    }
                    else
                    {
                        EventLog.WriteEntry("Application", "Tweet en échec pour l'article : " + article.Title, EventLogEntryType.Information);
                    }
                }
                #endregion

                #endregion

                return RedirectToAction("List", "Article", new { sitename = userSite });
            }
            return View();
        }
        #endregion

        #region - Create -
        [HttpGet]
        public ActionResult Create()
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
            ViewBag.UserName = u.UserName;
            ViewBag.SiteName = u.SiteName;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArticleCreate pArticle)
        {
            if (null != pArticle)
            {
                Article ar = Mapper.Map<ArticleCreate, Article>(pArticle);
                ArticlesContext.AddArticle(ar, User.Identity.Name);

                return RedirectToAction("Manage", "Article");
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateLink()
        {
            UserProfile u = UsersContext.GetUserByEmail(User.Identity.Name);
            ViewBag.UserName = u.UserName;
            ViewBag.SiteName = u.SiteName;

            return View();
        }

        [HttpPost]
        public ActionResult CreateLink(ArticleCreateLink pArticle)
        {
            if (null != pArticle)
            {
                Article ar = Mapper.Map<ArticleCreateLink, Article>(pArticle);

                ArticlesContext.AddArticle(ar, User.Identity.Name);

                return RedirectToAction("Manage", "Article");
            }
            return View();
        }
        #endregion

        #region - Delete -
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                ArticlesContext.DeleteArticle(id.Value);
            }
            return RedirectToAction("Manage");
        }
        #endregion

        #region - Téléchargements -
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ImageUpload(HttpPostedFileBase UploadPicture, int Id)
        {

            if (null != UploadPicture && UploadPicture.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads/Articles/Images"),
                                               Path.GetFileName(UploadPicture.FileName));

                ArticlesContext.UpdateArticleImage(Id, Path.Combine("", Path.GetFileName(UploadPicture.FileName)));

                UploadPicture.SaveAs(filePath);
            }
            return RedirectToAction("Edit", "Article", new { id = Id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase UploadFile, int Id)
        {

            if (null != UploadFile && UploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads/Articles/Files"),
                                               Path.GetFileName(UploadFile.FileName));

                ArticlesContext.UpdateArticleFile(Id, Path.Combine("../Uploads/Articles/Files", Path.GetFileName(UploadFile.FileName)));

                UploadFile.SaveAs(filePath);
            }
            return RedirectToAction("Edit", "Article", new { id = Id });
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Download(string file)
        {
            string filePath = HttpContext.Server.MapPath(file);

            FileInfo f = new FileInfo(file);

            return new FilePathResult(file, FileTools.GetMimeType(f.Extension));
        }
        #endregion

        [ChildActionOnly]
        public ActionResult PublishExternal()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            return PartialView("_PublishExternalPartial", externalLogins);
        }

        [AllowAnonymous]
        public ActionResult GetNextContent(int lastID, string siteName)
        {
            Thread.Sleep(500);
            List<ArticleIndex> articles = new List<ArticleIndex>();

            UserProfile u = UsersContext.GetUserBySiteName(siteName);

            foreach (Article a in ArticlesContext.GetUserPublishedArticles(u, lastID))
            {
                ArticleIndex ai = Mapper.Map<Article, ArticleIndex>(a);
                articles.Add(ai);
            }

            return PartialView("_ListArticlePartial", articles);
        }
    }
}
