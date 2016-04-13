using System;
using System.Net.Mail;
using System.Configuration;

namespace WH.MVC.Tools.Helpers
{
    /// <summary>
    /// Classe d'envoie des Emails.
    /// </summary>
    public class MailHelper
    {
        #region - Propriétés -
        /// <summary>
        /// Retourne ou définit l'email de l'expéditeur de l'email.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Retourne ou définit le nom de l'expéditeur de l'email.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Retourne ou définit l'email de destinataire de l'email.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Retourne ou définit l'objet de l'email.
        /// </summary>
        public string Subject { get; set; }

        private bool _isHtmlBody = false;
        /// <summary>
        /// Retourne ou définit si le corps de l'email est au format HTML.
        /// </summary>
        public bool IsHtmlBody
        {
            get { return _isHtmlBody; }
            set { _isHtmlBody = value; }
        }

        /// <summary>
        /// Retourne ou définit le corp de l'email.
        /// </summary>
        public string Body { get; set; }

        //private string smtp = "smtp.neuf.fr"; // "mail.ikoula.fr";
        /// <summary>
        /// Retourne ou définit le serveur SMTP d'envoie de l'email.
        /// </summary>
        public string Smtp { get; set; }
        #endregion

        #region - Constructeur -
        /// <summary>
        /// Consctructeur par défaut.
        /// </summary>
        public MailHelper()
        {
            this.Smtp = ConfigurationManager.AppSettings["MailSmtp"];
            this.From = ConfigurationManager.AppSettings["MailFrom"];
            this.To = ConfigurationManager.AppSettings["MailFrom"];
            this.Name = ConfigurationManager.AppSettings["MailName"];
        }

        /// <summary>
        /// Constructeur supplémentaire permettant l'affectation des proprietés de l'email.
        /// </summary>
        /// <param name="pFrom">Email de l'expéditeur</param>
        /// <param name="pName">Nom de l'expéditeur</param>
        /// <param name="pTo">Email du destinataire</param>
        /// <param name="pSubject">Sujet du mail</param>
        /// <param name="pHtmlBody">true ou false pour définir si le mail sera affiché en tant que page HTML</param>
        /// <param name="pBody">Le message du mail</param>
        public MailHelper(string pFrom, string pName, string pTo, string pSubject, Boolean pHtmlBody, string pBody)
        {
            this.Smtp = ConfigurationManager.AppSettings["MailSmtp"];
            this.From = pFrom;
            this.Name = pName;
            this.To = pTo;
            this.Subject = pSubject;
            this.IsHtmlBody = pHtmlBody;
            this.Body = pBody;
        }

        /// <summary>
        /// Constructeur simplifié permettant la détermination des propriétés à partir du fichier de configuration
        /// </summary>
        /// <param name="pTo">Email du destinataire</param>
        /// <param name="pSubject">Sujet du mail</param>
        /// <param name="pBody">Le message du mail</param>
        public MailHelper(string pTo, string pSubject, string pBody)
        {
            this.Smtp = ConfigurationManager.AppSettings["MailSmtp"];
            this.From = ConfigurationManager.AppSettings["MailFrom"];
            this.Name = ConfigurationManager.AppSettings["MailName"];
            this.To = pTo;
            this.Subject = pSubject;
            this.IsHtmlBody = true;
            this.Body = pBody;
        }
        #endregion

        /// <summary>
        /// Envoie un mail avec les paramêtres des proprietés.
        /// </summary>
        public void Send()
        {
            // On vérifie que les propriétés nécessaires à l'envoie de l'email ont déjà été définit.
            // On vérifie que le client n'est pas null
            if (null == this.From)
                throw new ArgumentNullException("From");
            // On vérifie que le client n'est pas null
            if (null == this.Name)
                throw new ArgumentNullException("Name");
            // On vérifie que le client n'est pas null
            if (null == this.To)
                throw new ArgumentNullException("To");
            // On vérifie que le client n'est pas null
            if (null == this.Subject)
                throw new ArgumentNullException("Subject");
            // On vérifie que le client n'est pas null
            if (null == this.Body)
                throw new ArgumentNullException("Body");

            MailAddress from = new MailAddress(this.From, this.Name);
            MailAddress to = new MailAddress(this.To);
            MailMessage message = new MailMessage(from, to);
            message.Subject = this.Subject;
            message.IsBodyHtml = this.IsHtmlBody;
            message.Body = this.Body;
            SmtpClient client = new SmtpClient(this.Smtp);
            client.Send(message);
        }
    }
}
