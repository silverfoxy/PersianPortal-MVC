using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersianPortal.Models;
using Microsoft.AspNet.Identity;


namespace PersianPortal.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /News/
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: /News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: /News/Create
        [Authorize(Roles = "Administrator,NewsAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,NewsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Title, string Body, string Tags, string Attachment, NewsType Type)
        {
            News news = new News()
            {
                Tags = Tags,
                Body = Body,
                Title = Title,
                Type = Type,
                PublishDate = DateTime.Now,
                AuthorId = User.Identity.GetUserId()
            };

            if (ModelState.IsValid)
            {
                //potentially unsafe
                var att = db.File.Where(f => f.URL.Contains(Attachment)).First();
                if (att != null)
                {
                    news.AttachmentId = att.Id;
                }
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: /News/Edit/5
        [Authorize(Roles = "Administrator,NewsAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                news = db.News.Find(id);
            else
                news = db.News.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Users can only manage their own news unless they are admin
        [HttpPost]
        [Authorize(Roles = "Administrator,NewsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Tags, string AttachmentURL, DateTime PublishDate, NewsType Type, string Body, string Title)
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator") || db.News.Find(id).AuthorId == User.Identity.GetUserId())
            {
                if (ModelState.IsValid)
                {
                    News news = new News()
                    {
                        Id = id,
                        Tags = Tags,
                        AttachmentId = db.File.Where(f => AttachmentURL.Contains(f.URL)).FirstOrDefault().Id,
                        Body = Body,
                        Title = Title,
                        Type = Type,
                    };
                    db.Entry(news).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(db.News.Find(id));
            }
            else
                return View();
        }

        // GET: /News/Delete/5
        [Authorize(Roles = "Administrator,NewsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                news = db.News.Find(id);
            else
                news = db.News.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,NewsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                news = db.News.Find(id);
            else
                news = db.News.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            db.News.Remove(news);
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
