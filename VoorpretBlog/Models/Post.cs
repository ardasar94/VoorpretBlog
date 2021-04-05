using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}