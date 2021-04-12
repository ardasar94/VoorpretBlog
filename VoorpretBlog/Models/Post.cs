using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class Post
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        [Required]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public DateTime? CreationDate { get; set; }
        public int TagId { get; set; }
        public virtual  ICollection<Tag> Tags { get; set; }
    }
}