using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult New(PostViewModel postVm, HttpPostedFileBase image)
        {
            ImageErrorChecks(image);
            if (ModelState.IsValid)
            {
                Post post = new Post()
                {

                    AuthorId = User.Identity.GetUserId(),
                    ImagePath = ImageUpload(image),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            DeleteImage(post.ImagePath);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase image)
        {
            ImageErrorChecks(image);

            if (post != null)
            {
                var imagePath = ImageUpload(image);
                if (imagePath != null)
                {
                    DeleteImage(post.ImagePath);
                    post.ImagePath = imagePath;
                }
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(post);
        }


        private void DeleteImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                var delPath = Path.Combine(Server.MapPath("~/Images/Uploads"), imagePath);

                if (System.IO.File.Exists(delPath))
                {
                    System.IO.File.Delete(delPath);
                }
            }
        }

        private void ImageErrorChecks(HttpPostedFileBase image)
        {
            string[] allowed = { ".jpg", ".jpeg", ".png" };
            if (image != null)
            {
                if (!allowed.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    ModelState.AddModelError("resim", "İzin verilen dosya uzantıları: .jpg, .jpeg, .png");
                }
                else if (image.ContentLength > 1000 * 1000)
                {
                    ModelState.AddModelError("resim", "Resim boyutu 1mb'dan küçük olmalıdır");
                }

            }
        }

        private string ImageUpload(HttpPostedFileBase image)
        {
            if (image == null)
                return null;
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var uploadFilePath = Server.MapPath("~/Images/Uploads");
            var kaydetYol = Path.Combine(uploadFilePath, fileName);
            image.SaveAs(kaydetYol);
            return fileName;
        }

    }
}