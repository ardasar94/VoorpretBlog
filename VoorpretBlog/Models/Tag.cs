using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public override string ToString()
        {

            return TagName;
        }
    }
}