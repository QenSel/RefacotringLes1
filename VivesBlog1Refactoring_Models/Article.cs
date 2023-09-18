using System.ComponentModel.DataAnnotations;
using System;

namespace VivesBlog_Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Person Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}


