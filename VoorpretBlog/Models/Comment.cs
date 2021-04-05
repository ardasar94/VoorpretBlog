using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        //
        [Required]
        public DateTime? CreationDate { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Post Post { get; set; }
        public int AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

    }
}