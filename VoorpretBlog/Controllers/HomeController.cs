using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoorpretBlog.Models;

namespace VoorpretBlog.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int page = 1)
        {
            IQueryable<Post> query = db.Posts;
            int totalItems = query.Count();
            int pageSize = 3;
            int totalPages = (int)Math.Ceiling(db.Posts.Count() / (decimal)pageSize);
            var posts = query
                .OrderByDescending(x => x.CreationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new HomeViewModel()
            {
                FullPosts = db.Posts.OrderByDescending(x => x.CreationDate).ToList(),
                Posts = posts,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    ItemsOnPage = posts.Count,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    HasNext = page < totalPages,
                    HasPrevious = page > 1
                }
            };
            //db.Posts.OrderByDescending(x => x.CreationDate).ToList()
            return View(vm);
        }

    }
}