using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Model
{
    [Table("Article")]
    public class Article : BO
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Content { get; set; }
        public DateTime? Publication { get; set; }
        public DateTime Creation { get; set; }
        public int UserId { get; set; }
        public UserProfile User { get; set; }
        public double Visits { get; set; }
        public string Video { get; set; }
        public string Picture { get; set; }
        public string File { get; set; }
        public string LinkUrl { get; set; }
        public bool isLink { get; set; }
        public DateTime? FbPublication { get; set; }
        public DateTime? TwitterPublication { get; set; }
        public DateTime? GooglePublication { get; set; }
        public string Category { get; set; }
    }
}
