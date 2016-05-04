using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetPress.Models;
using Microsoft.AspNet.Identity;

namespace NetPress.Controllers
{
    [Authorize]

    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Post
        public ActionResult Index()
        {
            IList<Posts> posts = db.Posts.ToList();

            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();

            return View(posts);
        }

        [AllowAnonymous]
        public ActionResult Search(string searchString,string searchID)
        {
            IList<Posts> posts = db.Posts.ToList();

            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();

            if (searchString != null)
            {
                posts = posts.Where(p => p.category.Contains(searchString)).ToList();
            }
            if (searchID != null)
            {
                posts = posts.Where(p => p.UserID.Equals(searchID)).ToList();
            }
            return View(posts);
        }

        public ActionResult Manage(string contentStatus)
        {
            IList<Posts> posts = db.Posts.ToList();

            //get authenticated user id and filter
            posts = posts.Where(p => p.UserID.Equals(User.Identity.GetUserId())).ToList();

            switch (contentStatus)
            {
                case "Published":
                    posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();
                    break;

                case "Draft":
                    posts = posts.Where(p => p.status.Equals(Posts.Status.Draft)).ToList();
                    break;

                case "Archived":
                    posts = posts.Where(p => p.status.Equals(Posts.Status.Archived)).ToList();
                    break;
                default:

                    break;

            };

            return View(posts);
        }
        // GET: Post/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postID,title,content,category,status,dateCreated,lastModified")] Posts posts)
        {
            posts.dateCreated = DateTime.Now;
            posts.lastModified = DateTime.Now;

            //var UserID = User.Identity.GetUserId;

            posts.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Posts.Add(posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posts);
        }

        // GET: Post/Edit/5
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(Posts posts)
        {
            if (ModelState.IsValid)
            {

                posts.lastModified = DateTime.Now;
                db.Entry(posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            return View(posts);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}