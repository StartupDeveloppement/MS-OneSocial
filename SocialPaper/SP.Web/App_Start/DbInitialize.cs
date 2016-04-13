using SP.Web.Context;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;

namespace SP.Web
{
    public class DbInitialize
    {
        public static void InitializeAll()
        {
            try
            {
                using (var context = new UsersContext())
                {
                    // Création de la base de données si besoin
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }

                    // Initialisation de la Membership database
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "Id", "Email", autoCreateTables: true);

                    // Application des migrations
                    /*
                     * Le type de l'initialiseur est définit dans le fichier de configuration
                     */
                    
                    context.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    }
}