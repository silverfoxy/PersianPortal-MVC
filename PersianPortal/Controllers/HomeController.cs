using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersianPortal.Models;
using Microsoft.AspNet.Identity;

namespace PersianPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var roles = db.Users.Find(userId).Roles.ToList();
                if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                {
                    ViewBag.CanViewNewsPanel = true;
                }
                else
                    ViewBag.CanViewNewsPanel = false;
            }
            else
                ViewBag.CanViewNewsPanel = false;
            return View(db.News.ToList());
        }
        [Authorize(Roles="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}