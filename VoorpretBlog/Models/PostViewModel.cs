using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Tags = new HashSet<Tag>();
        }
        public string ImagePath { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}