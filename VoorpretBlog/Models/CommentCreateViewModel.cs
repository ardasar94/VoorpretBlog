using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class CommentCreateViewModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }
    }
}