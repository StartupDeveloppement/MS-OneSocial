using SP.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SP.Web.Context
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        #region - GetUserByEmail -
        /// <summary>
        /// Permet de récupérer le site de l'utilisateur à partir de son id
        /// </summary>
        /// <param name="pEmail">L'id de l'utilisateur</param>
        /// <returns>Le site</returns>
        public static string GetSiteNameById(int pId)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                SP.Model.UserProfile user = ctx.UserProfiles.FirstOrDefault(u => u.Id == pId);

                return user.SiteName;
            }
        }
        #endregion

        #region - GetUserByEmail -
        /// <summary>
        /// Permet de récupérer l'utilisateur à partir de son email
        /// </summary>
        /// <param name="pEmail">L'email de l'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        public static UserProfile GetUserByEmail(string pEmail)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                SP.Model.UserProfile user = ctx.UserProfiles.FirstOrDefault(u => u.Email.Equals(pEmail));

                return user;
            }
        }
        #endregion

        #region - GetUserBySiteName -
        /// <summary>
        /// Permet de récupérer l'utilisateur à partir de son nom du site (le nom du site devrait être unique)
        /// </summary>
        /// <param name="pEmail">Le nom du site de l'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        public static UserProfile GetUserBySiteName(string pSiteName)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                SP.Model.UserProfile user = ctx.UserProfiles.FirstOrDefault(u => u.SiteName.Equals(pSiteName));

                return user;
            }
        }
        #endregion

        #region - GetUserName -
        /// <summary>
        /// Permet de récupérer le nom de l'utilisateur à partir de son email.
        /// </summary>
        /// <param name="pEmail">L'email de l'utilisateur</param>
        /// <returns>Le nom de l'utilisateur</returns>
        public static string GetUserName(string pEmail)
        {
            UserProfile user = GetUserByEmail(pEmail);
            if (null != user)
                return user.UserName;
            else
                return string.Empty;
        }
        #endregion

        #region - GetUserSite -
        /// <summary>
        /// Permet de récupérer le site de l'utilisateur à partir de son email
        /// </summary>
        /// <param name="pEmail">L'email de l'utilisateur</param>
        /// <returns>Le site de l'utilisateur</returns>
        public static string GetUserSite(string pEmail)
        {
            UserProfile user = GetUserByEmail(pEmail);
            if (null != user)
                return user.SiteName;
            else
                return string.Empty;
        }
        #endregion

        public static void UpdateUserImage(int pUserId, string pImageUrl)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                UserProfile user = ctx.UserProfiles.FirstOrDefault(a => a.Id == pUserId);

                user.Image = pImageUrl;

                ctx.SaveChanges();
            }
        }

        public static void UpdateUserCv(int pUserId, string pCvUrl)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                UserProfile user = ctx.UserProfiles.FirstOrDefault(a => a.Id == pUserId);

                user.Cv = pCvUrl;

                ctx.SaveChanges();
            }
        }

        public static void UpdateUserDescription(int pUserId, string pDescription)
        {
            using (SP.Web.Context.UsersContext ctx = new SP.Web.Context.UsersContext())
            {
                UserProfile user = ctx.UserProfiles.FirstOrDefault(a => a.Id == pUserId);

                user.Description = pDescription;

                ctx.SaveChanges();
            }
        }

        public static string GetUniqueSite(string pName)
        {
            string site = pName.Trim().ToLower().Replace(" ", "-");
            int i = 0;

            string countSite = site;

            while (null != GetUserBySiteName(countSite))
            {
                ++i;
                countSite = site + i.ToString();
            }
            return countSite;
        }
    }
}
