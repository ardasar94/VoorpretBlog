    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class HomeViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Post> FullPosts { get; set; }

        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}