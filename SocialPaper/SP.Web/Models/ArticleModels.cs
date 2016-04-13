using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WH.MVC.Tools.Commun;

namespace SP.Web.Models
{
    public class ArticleLink
    {
        public int Id { get; set; }
        
        private string title;
        public string Title 
        {
            get
            {
                if(this.title.Length > 40)
                {
                    string s = this.title.Substring(0, 40);
                    s += " ...";
                    return s;
                }
                else
                    return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string Site { get; set; }

        public string HtmTitle
        {
            get
            {
                return HtmlTools.HtmTitle(this.Title);
            }
        }
    }

    public class ArticleIndex
    {
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? Publication { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string isPublish { get; set; }
        public bool isLink { get; set; }
        public string LinkUrl { get; set; }
        public string HtmTitle
        {
            get
            {
                return HtmlTools.HtmTitle(this.Title);
            }
        }
    }

    public class ArticleContent
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Video { get; set; }
        public string File { get; set; }
        public string LinkUrl { get; set; }
        public bool isLink { get; set; }

        public string HtmTitle
        {
            get
            {
                return HtmlTools.HtmTitle(this.Title);
            }
        }
    }

    public class ArticleCreate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [Display(Name = "Titre")]
        [StringLength(140)]
        public string Title { get; set; }

        [Display(Name = "Catégorie")]
        [StringLength(30)]
        public string Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le résumé est obligatoire")]
        [Display(Name = "Résumé")]
        public string Abstract { get; set; }

        [Display(Name = "Url de la vidéo")]
        public string Video { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le contenu de l'article est obligatoire")]
        [Display(Name = "Article")]
        public string Content { get; set; }
    }

    public class ArticleCreateLink
    {
        [Required(ErrorMessage = "Le titre est obligatoire")]
        [Display(Name = "Titre")]
        [StringLength(140)]
        public string Title { get; set; }

        [StringLength(30)]
        [Display(Name = "Catégorie")]
        public string Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le résumé est obligatoire")]
        [Display(Name = "Résumé")]
        public string Abstract { get; set; }

        [Required(ErrorMessage = "L'Url de l'article est obligatoire")]
        [Display(Name = "Url de l'article", Description="Vous pouvez lier n'importe quel article existant sur un autre site. Il suffit de copier/coller son url ici.")]
        public string LinkUrl { get; set; }
    }

    public class ArticleEdit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(140)]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [StringLength(30)]
        [Display(Name = "Catégorie")]
        public string Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le résumé est obligatoire")]
        [Display(Name = "Résumé")]
        public string Abstract { get; set; }

        [Display(Name = "Url de la vidéo")]
        public string Video { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le contenu de l'article est obligatoire")]
        [Display(Name = "Article")]
        public string Content { get; set; }
    }

    public class ArticleEditLink
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [Display(Name = "Titre")]
        [StringLength(140)]
        public string Title { get; set; }

        [StringLength(30)]
        [Display(Name = "Catégorie")]
        public string Category { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Le résumé est obligatoire")]
        [Display(Name = "Résumé")]
        public string Abstract { get; set; }

        [Required(ErrorMessage = "L'Url de l'article est obligatoire")]
        [Display(Name = "Url de l'article", Description = "Vous pouvez lier n'importe quel article existant sur un autre site. Il suffit de copier/coller son url ici.")]
        public string LinkUrl { get; set; }

    }

    public class ArticlePublish
    {
        public int Id { get; set; }

        public ArticleContent Content { get; set; }

        public bool isPublished { get; set; }
        [Display(Name = "Républier sur mon site")]
        public bool Publish { get; set; }

        public bool isFbPublished { get; set; }
        public bool isFacebook { get; set; }
        [Display(Name = "Publier également sur Facebook")]
        public bool PublishOnFacebook { get; set; }

        public bool isTwitterPublished { get; set; }
        public bool isTwitter { get; set; }
        [Display(Name = "Publier également sur Twitter")]
        public bool PublishOnTwitter { get; set; }

        public bool isGooglePublished { get; set; }
        public bool isGoogle { get; set; }
        [Display(Name = "Publier également sur Google")]
        public bool PublishOnGoogle { get; set; }
    }

    public class AboutEdit
    {
        public int Id { get; set; }
        [Display(Name = "Ma présentation")]
        public string Description { get; set; }
        [Display(Name = "Télécharger votre image")]
        public string Image { get; set; }
        [Display(Name = "Télécharger votre CV")]
        public string Cv { get; set; }
    }
}