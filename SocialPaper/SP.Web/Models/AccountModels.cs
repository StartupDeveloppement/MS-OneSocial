using System.ComponentModel.DataAnnotations;

namespace SP.Web.Models
{
    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage="Le nom est obligatoire")]
        [Display(Name = "Nom & Prénom", Description = "Une page sera créer à votre nom dès votre inscription et vous permettra de publier vos articles.")]
        public string UserName { get; set; }

        [Required(ErrorMessage="L'email est obligatoire")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Merci de saisir une adresse email valide")]
        public string Email { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage="L'ancien mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Ancien")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage="Le nouveau mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le mot de passe doit être supérieur à 5 caractères", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("NewPassword", ErrorMessage = "Le mot de passe et sa confirmation sont différents.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage="L'email est obligatoire")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Merci de saisir une adresse email valide")]
        public string Email { get; set; }

        [Required(ErrorMessage="Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage="Le nom est obligatoire")]
        [Display(Name = "Nom & Prénom", Description="Une page sera créer à votre nom dès votre inscription et vous permettra de publier vos articles.")]
        public string UserName { get; set; }

        [Required(ErrorMessage="L'email est obligatoire")]
        [Display(Name = "Email", Description="Votre email sera votre identifiant unique et vous permet de vous identifier.")]
        [EmailAddress(ErrorMessage= "Merci de saisir une adresse email valide")]
        public string Email { get; set; }

        [Required(ErrorMessage="Le mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le mot de passe doit être supérieur à 5 caractères", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation sont différents.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
