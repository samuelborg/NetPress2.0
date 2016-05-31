﻿using System;
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
        public ActionResult Index()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //Get all posts and store them in a list
            IList<Posts> posts = db.Posts.ToList();

            //Keep only the posts whose status is marked Published
            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();

            //Re-order the posts in descending order according to date created
            posts.OrderByDescending(p => p.dateCreated);

            var model = new List<ViewPosts>();

            
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

            return View(model);
        }

        /*
         *Search controller
         * an take 2 values as parameters, 
         * First for the category 
         * Second for the ID of the user   
             */
        [AllowAnonymous]
        public ActionResult Search(string searchString, string searchID)
        {
            //Get all posts and store them in a list
            IList<Posts> posts = db.Posts.ToList();

            //Keep only the posts whose status is marked Published
            posts = posts.Where(p => p.status.Equals(Posts.Status.Published)).ToList();

           
            if (searchString != null)
            {  //User wants to filter by category
                posts = posts.Where(p => p.category.Equals(searchString,StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            if (searchID != null)
            {//User wants to filter by user ID
                posts = posts.Where(p => p.UserID.Equals(searchID)).ToList();
            }

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var model = new List<ViewPosts>();

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
            return View("Index",model);
        }

        /*
         * Manage Controller
         * Displays all the posts of the logged in user
         * If user is Admin, display all posts by all users
         * 
         * Can filter posts by Status using the parameter
       */
        public ActionResult Manage(string contentStatus)
        {
            //Get all posts and store them in a list
            IList<Posts> posts = db.Posts.ToList();

            //If user is of type Admin, get all posts made by all authors
            //If normal user with role "Author", filter by the ID of the logged in user

            if(!User.IsInRole("Admin"))
                posts = posts.Where(p => p.UserID.Equals(User.Identity.GetUserId())).ToList();

            //Get value of parameter and filter accordingly
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