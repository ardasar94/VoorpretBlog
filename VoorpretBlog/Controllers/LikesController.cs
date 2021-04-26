using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoorpretBlog.Models;

namespace VoorpretBlog.Controllers
{
    public class LikesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Likes()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Likes.ToList());
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddToLikes(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            post.Likes.Add(new Like
            {
                Post = post,
                User = user,
                UserId = user.Id
            });
            db.SaveChanges();
            //return View(user.Likes.ToList());
            return Json(new { success = true });

        }

        public ActionResult RemoveFromFavorites(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            var like = db.Likes.FirstOrDefault(x =>x.UserId==user.Id && x.Post.Id==id);
            post.Likes.Remove(like);
            db.SaveChanges();
            return Json(new { success = true });

        }
    }
}