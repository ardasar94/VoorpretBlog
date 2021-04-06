using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoorpretBlog.Models;

namespace VoorpretBlog.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Post

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userId);

            return View(user.Posts.ToList());
        }


        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(PostViewModel postVm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post()
                {
                    AuthorId = User.Identity.GetUserId(),
                    ImagePath = postVm.ImagePath,
                    Title = postVm.Title,
                    Content = postVm.Content,
                    Tags = postVm.Tags,
                    CreationDate = DateTime.Now
                };

                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postVm);
        }
    }
}