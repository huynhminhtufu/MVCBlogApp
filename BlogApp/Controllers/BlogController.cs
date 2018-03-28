using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private BlogDBEntities db = new BlogDBEntities();

        // GET: Blog
        public ActionResult Index()
        {
            //var tbl_Blog = db.Tbl_Blog.Include(t => t.Tbl_Category).Include(t => t.Tbl_User);
            int UserID = int.Parse(Session["UserID"].ToString());
            var ListBlog = from blog in db.Tbl_Blog
                           where blog.UserID == UserID
                           select blog;
            return View(ListBlog.ToList());
        }

        public ActionResult Cate(int id)
        {
            var tbl_Cate = from blog in db.Tbl_Blog
                           join category in db.Tbl_Category
                           on blog.CategoryID equals category.CategoryID
                           where category.CategoryID == id
                           orderby blog.BlogID descending
                           select blog;
            var CateInfo = from category in db.Tbl_Category
                           where category.CategoryID == id
                           select category;
            ViewBag.CateInfo = CateInfo.ToList();
            return View(tbl_Cate.ToList());
        }

        public ActionResult Search(string search)
        {
            search = search.Trim();
            ViewBag.SearchKeyword = search;
            var blogSearch = (from blog in db.Tbl_Blog
                              where blog.BlogTitle.Contains(search) || blog.BlogContent.Contains(search)
                              join user in db.Tbl_User on blog.UserID equals user.UserID
                              orderby blog.BlogID descending
                              select blog);
            ViewBag.SearchResult = blogSearch.ToList();
            return View();
        }

        public ActionResult Rate(int id, int star)
        {
            if (Session["Blog" + id] == null)
            {
                Tbl_Blog blog = db.Tbl_Blog.Find(id);
                blog.NumOfRating += 1;
                blog.RatingPoint += star;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                Session["Blog" + id] = 1;
            }
            return RedirectToAction("Details/" + id);
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Blog tbl_Blog = db.Tbl_Blog.Find(id);
            if (tbl_Blog == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Tbl_Category, "CategoryID", "CategoryName");
            ViewBag.UserID = new SelectList(db.Tbl_User, "UserID", "Username");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogID,BlogTitle,BlogContent,UserID,CreatedDate,RatingPoint,NumOfRating,CategoryID")] Tbl_Blog tbl_Blog)
        {
            if (ModelState.IsValid)
            {
                tbl_Blog.NumOfRating = 0;
                tbl_Blog.RatingPoint = 0;
                tbl_Blog.CreatedDate = DateTime.Now;
                tbl_Blog.UserID = int.Parse(Session["UserID"].ToString());
                db.Tbl_Blog.Add(tbl_Blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Tbl_Category, "CategoryID", "CategoryName", tbl_Blog.CategoryID);
            ViewBag.UserID = new SelectList(db.Tbl_User, "UserID", "Username", tbl_Blog.UserID);
            return View(tbl_Blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Blog tbl_Blog = db.Tbl_Blog.Find(id);
            if (tbl_Blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Tbl_Category, "CategoryID", "CategoryName", tbl_Blog.CategoryID);
            ViewBag.UserID = new SelectList(db.Tbl_User, "UserID", "Username", tbl_Blog.UserID);
            return View(tbl_Blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogID,BlogTitle,BlogContent,UserID,CreatedDate,RatingPoint,NumOfRating,CategoryID")] Tbl_Blog tbl_Blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Tbl_Category, "CategoryID", "CategoryName", tbl_Blog.CategoryID);
            ViewBag.UserID = new SelectList(db.Tbl_User, "UserID", "Username", tbl_Blog.UserID);
            return View(tbl_Blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Blog tbl_Blog = db.Tbl_Blog.Find(id);
            if (tbl_Blog == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Blog tbl_Blog = db.Tbl_Blog.Find(id);
            db.Tbl_Blog.Remove(tbl_Blog);
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
