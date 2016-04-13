using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Model
{
    [Table("UserProfile")]
    public class UserProfile : BO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SiteName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Cv { get; set; }

        public List<Article> Articles { get; set; }
    }
}
