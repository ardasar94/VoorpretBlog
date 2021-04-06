﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Models
{
    public class PostViewModel
    {
        public string ImagePath { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public List<string> Tags { get; set; }
    }
}