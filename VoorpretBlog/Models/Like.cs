using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}