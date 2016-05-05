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
using NetPress.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NetPress.Controllers
{
    [Authorize]

    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Post

        //public ActionResult Index(string searchString, string searchID)
        //{ 
        //    IList<Posts> posts = db.Posts.ToList();

        //    posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();


        //    if(searchString!= null)
        //    {
        //        posts = posts.Where(p => p.category.Contains(searchString)).ToList();
           
        //    }
        //    if (searchID != null)
        //    {
        //       posts = posts.Where(p => p.UserID.Equals(searchID)).ToList();

        //    }

        //    return View(posts);
        //}


        public ActionResult Index(string searchString, string searchID)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var posts = db.Posts.Where(p => p.status == Posts.Status.Published).ToList();
            var model = new List<ViewPosts>();

            //if (searchString != null)
            //{
            //    posts = posts.Where(p => p.category.Contains(searchString)).ToList();

            //}

            //if (searchID != null)
            //{
            //    posts = posts.Where(p => p.UserID.Equals(searchID)).ToList();
            //}
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult SearchCategory(string searchString)
        {
            IList<Posts> posts = db.Posts.ToList();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var model = new List<ViewPosts>();

            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();


            if (searchString != null)
            {
                posts = posts.Where(p => p.category.Contains(searchString)).ToList();

            }

            foreach (var p in posts)
            {
                var author = manager.FindById(p.UserID);
                model.Add(
                    new ViewPosts()
                    {
                        category = p.category,
                        UserFullName = author.Name + " " + author.Surname,
                        content = p.content,
                        dateCreated = p.dateCreated,
                        postID = p.postID,
                        title = p.title,
                        UserID = p.UserID,
                        lastModified = p.lastModified
                    });
            }

            return View(posts);
        }
        [AllowAnonymous]
        public ActionResult SearchID(string searchID)
        {
            IList<Posts> posts = db.Posts.ToList();
                        var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var model = new List<ViewPosts>();

            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();

            if (searchID != null)
            {
                posts = posts.Where(p => p.UserID.Equals(searchID)).ToList();

            }

            foreach (var p in posts)
            {
                var author = manager.FindById(p.UserID);
                model.Add(
                    new ViewPosts()
                    {
                        category = p.category,
                        UserFullName = author.Name + " " + author.Surname,
                        content = p.content,
                        dateCreated = p.dateCreated,
                        postID = p.postID,
                        title = p.title,
                        UserID = p.UserID,
                        lastModified = p.lastModified
                    });
            }

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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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