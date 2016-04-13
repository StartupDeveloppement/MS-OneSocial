using SP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SP.Web.Context
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ArticlesContext>(null);
        }

        public DbSet<Article> Articles { get; set; }

        #region - GetArticleById -
        /// <summary>
        /// Permet de sélectionner un article à partir de son ID
        /// </summary>
        /// <param name="pId">L'Id de l'article</param>
        /// <returns></returns>
        public static Article GetArticleById(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.FirstOrDefault(a => a.Id == pId);
            }
        }

        /// <summary>
        /// Permet de sélectionner un article publié pour un site à partir de son id
        /// </summary>
        /// <param name="pSiteName">Le nom du site</param>
        /// <param name="pId">L'id de l'article</param>
        /// <returns></returns>
        public static Article GetPublishedArticleById(string pSiteName, int pId)
        {
            return GetPublishedArticleById(UsersContext.GetUserBySiteName(pSiteName), pId);
        }

        /// <summary>
        /// Permet de sélectionner un article pour un utilisateur à partir de son id
        /// </summary>
        /// <param name="pUser">L'utilisateur</param>
        /// <param name="pId">L'id de l'article</param>
        /// <returns></returns>
        public static Article GetPublishedArticleById(UserProfile pUser, int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.FirstOrDefault(a => a.UserId == pUser.Id && a.Publication != null && a.Id == pId);
            }
        }
        #endregion

        #region - GetPublishedArticles -
        /// <summary>
        /// Permet de sélectionner les articles publiés
        /// </summary>
        public static List<Article> GetSitePublishedArticles(string pSiteName)
        {
            return GetUserPublishedArticles(UsersContext.GetUserBySiteName(pSiteName));
        }

        /// <summary>
        /// Permet de sélectionner les articles publiés d'un utilisateur donné
        /// <param name="pUser">L'utilisateur</param>
        /// </summary>
        public static List<Article> GetUserPublishedArticles(UserProfile pUser)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(a => a.UserId == pUser.Id && a.Publication > DateTime.MinValue).OrderByDescending(a => a.Publication).ToList();
            }
        }

        /// <summary>
        /// Permet de sélectionner les articles publiés d'un utilisateur donné
        /// <param name="pUser">L'utilisateur</param>
        /// <param name="pPageIndex">pPageIndex</param>
        /// </summary>
        public static List<Article> GetUserPublishedArticles(UserProfile pUser, int pPageIndex)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(a => a.UserId == pUser.Id && a.Publication > DateTime.MinValue).OrderByDescending(a => a.Publication).Skip(pPageIndex * 10).Take(10).ToList();
            }
        }

        /// <summary>
        /// Permet de sélectionner les articles publiés d'un utilisateur donné
        /// <param name="pUser">L'utilisateur</param>
        /// </summary>
        public static List<Article> GetUserPublishedArticles(string pEmail)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                UserProfile u = UsersContext.GetUserByEmail(pEmail);
                return ctx.Articles.Where(a => a.UserId == u.Id && a.Publication > DateTime.MinValue).OrderByDescending(a => a.Publication).ToList();
            }
        }
        #endregion

        #region - GetUserArticles -
        /// <summary>
        /// Permet de sélectionner les articles d'un utilisateur donné
        /// <param name="pUser">L'utilisateur</param>
        /// </summary>
        public static List<Article> GetUserArticles(UserProfile pUser)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(a => a.UserId == pUser.Id).OrderByDescending(a => a.Creation).ToList();
            }
        }

        /// <summary>
        /// Permet de sélectionner les articles d'un utilisateur à partir de son email
        /// </summary>
        /// <param name="pEmail"></param>
        /// <returns></returns>
        public static List<Article> GetUserArticles(string pEmail)
        {
            return GetUserArticles(UsersContext.GetUserByEmail(pEmail)).ToList();
        }
        #endregion

        public static void AddArticle(Article pArticle, string pEmail)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                pArticle.UserId = UsersContext.GetUserByEmail(pEmail).Id;
                pArticle.Creation = DateTime.Now;

                ctx.Articles.Add(pArticle);

                ctx.SaveChanges();
            }
        }

        public static void UpdateArticle(Article pArticle)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pArticle.Id);

                article.Abstract = pArticle.Abstract;
                article.Content = pArticle.Content;
                article.Title = pArticle.Title;
                article.Video = pArticle.Video;
                article.LinkUrl = pArticle.LinkUrl;
                article.Category = pArticle.Category;

                ctx.SaveChanges();
            }
        }

        public static Article PublishArticle(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                article.Publication = DateTime.Now;

                ctx.SaveChanges();

                return article;
            }
        }

        public static void MarkFbPublication(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                article.FbPublication = DateTime.Now;

                ctx.SaveChanges();
            }
        }

        public static void MarkTwitterPublication(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                article.TwitterPublication = DateTime.Now;

                ctx.SaveChanges();
            }
        }

        public static void MarkGooglePublication(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                article.GooglePublication = DateTime.Now;

                ctx.SaveChanges();
            }
        }

        public static void DeleteArticle(int pId)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                ctx.Articles.Remove(article);

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Permet de sélectionner les 5 derniers articles publiés
        /// </summary>
        public static ICollection<Article> GetLastPublishedArticles()
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(c => c.Publication > DateTime.MinValue).OrderByDescending(c => c.Publication).Take(5).ToList();
            }
        }

        public static void IncrementeVisits(Article pArticle)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article article = ctx.Articles.FirstOrDefault(a => a.Id == pArticle.Id);

                article.Visits += 1;

                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Permet de sélectionner les 5 articles les plus populaires
        /// </summary>
        public static ICollection<Article> GetPopularArticles()
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(c => c.Visits > 0 && c.Publication > DateTime.MinValue).OrderByDescending(c => c.Visits).Take(5).ToList();
            }
        }

        public static void UpdateArticleImage(int pId, string pImageUrl)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article user = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                user.Picture = pImageUrl;

                ctx.SaveChanges();
            }
        }

        public static void UpdateArticleFile(int pId, string pFileUrl)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                Article user = ctx.Articles.FirstOrDefault(a => a.Id == pId);

                user.File = pFileUrl;

                ctx.SaveChanges();
            }
        }

        public static List<string> GetUserCategories(string pUserEmail)
        {
            List<string> result = new List<string>();

            List<Article> articles = ArticlesContext.GetUserPublishedArticles(pUserEmail);

            var categories = articles.GroupBy(c => c.Category);

            foreach (var item in categories)
            {
                if (!string.IsNullOrEmpty(item.Key))
                    result.Add(item.Key);
                if (result.Count() == 10)
                    break;
            }

            return result;
        }

        public static List<string> GetSiteCategories(string pSiteName)
        {
            List<string> result = new List<string>();

            List<Article> articles = ArticlesContext.GetSitePublishedArticles(pSiteName);

            var categories = articles.GroupBy(c => c.Category);

            foreach (var item in categories)
            {
                if (!string.IsNullOrEmpty(item.Key))
                    result.Add(item.Key);
                if (result.Count() == 10)
                    break;
            }

            return result;
        }

        public static IEnumerable<Article> GetUserPublishedArticlesByCategory(UserProfile pUser, string pCategory)
        {
            using (SP.Web.Context.ArticlesContext ctx = new SP.Web.Context.ArticlesContext())
            {
                return ctx.Articles.Where(a => a.UserId == pUser.Id && a.Publication > DateTime.MinValue && a.Category.Equals(pCategory)).OrderByDescending(a => a.Publication).ToList();
            }
        }
    }
}
