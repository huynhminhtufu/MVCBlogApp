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
    public class UserController : Controller
    {
        private BlogDBEntities db = new BlogDBEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Tbl_User.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_User tbl_User = db.Tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                using (BlogDBEntities db = new BlogDBEntities())
                {
                    var obj = db.Tbl_User.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.Username.ToString();
                        return RedirectToRoute("BlogIndex");
                    }
                }
            }
            ViewBag.Username = username.ToString();
            ViewBag.Status = 0;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Password,Email,Type")] Tbl_User tbl_User)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_User.Add(tbl_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_User);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_User tbl_User = db.Tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Password,Email,Type")] Tbl_User tbl_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_User);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_User tbl_User = db.Tbl_User.Find(id);
            if (tbl_User == null)
            {
                return HttpNotFound();
            }
            return View(tbl_User);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_User tbl_User = db.Tbl_User.Find(id);
            db.Tbl_User.Remove(tbl_User);
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
