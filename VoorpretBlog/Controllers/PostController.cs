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
            ViewBag.Tags = new SelectList(db.Tags, "Id", "TagName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(PostViewModel postVm, HttpPostedFileBase image, string[] tags)
        {
            ImageErrorChecks(image);
            if (postVm != null)
            {
                Post post = new Post()
                {
                    AuthorId = User.Identity.GetUserId(),
                    ImagePath = ImageUpload(image),
                    Title = postVm.Title,
                    Tags = postVm.Tags,
                    Content = postVm.Content,
                    CreationDate = DateTime.Now
                };

                var allTags = tags[1];
                string[] tagFinal = allTags.Split(',');
                for (int i = 0; i < tagFinal.Length; i++)
                {
                    var oneTag = tagFinal[i];
                    if (!db.Tags.Any(x => x.TagName == oneTag))
                    {
                        Tag addedTag = new Tag() { TagName = tagFinal[i] };
                        db.Tags.Add(addedTag);
                        post.Tags.Add(addedTag);
                    }
                    else
                    {
                        Tag tagAlreadyCreated = db.Tags.FirstOrDefault(x => x.TagName == oneTag);
                        post.Tags.Add(tagAlreadyCreated);
                    }
                }
                //if (Id != null)
                //{
                //    for (int i = 0; i < Id.Length; i++)
                //    {
                //        Tag tag = db.Tags.Find(Id[i]);
                //        post.Tags.Add(tag);
                //    }
                //}
                db.Posts.Add(post);
                db.SaveChanges();
                ViewBag.Tags = db.Tags.ToList();
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
            ViewBag.Tags = post.Tags.ToList();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase image, string[] tags)
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
                var allTags = tags[1];
                string[] tagFinal = allTags.Split(',');
                for (int i = 0; i < tagFinal.Length; i++)
                {
                    var oneTag = tagFinal[i];
                    if (!db.Tags.Any(x => x.TagName == oneTag))
                    {
                        Tag addedTag = new Tag() { TagName = tagFinal[i] };
                        db.Tags.Add(addedTag);
                        post.Tags.Add(addedTag);
                    }
                    else
                    {
                        Tag tagAlreadyCreated = db.Tags.FirstOrDefault(x => x.TagName == oneTag);
                        post.Tags.Add(tagAlreadyCreated);
                    }
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