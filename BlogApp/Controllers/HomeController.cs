using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (BlogDBEntities db = new BlogDBEntities())
            {
                var BlogQuery = (from blog in db.Tbl_Blog
                           orderby blog.BlogID descending
                           select blog).Take(4);
                return View(BlogQuery.ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}