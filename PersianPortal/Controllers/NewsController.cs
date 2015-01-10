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
            return View(new NewsViewModel());
        }

        // POST: /News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,NewsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsViewModel nvm)
        {
            News news = nvm.News;
            news.PublishDate = DateTime.Now;
            int ntid = int.Parse(nvm.Type);
            news.Type = db.NewsType.Where(nt => nt.Id == ntid).FirstOrDefault();
            news.AuthorId = User.Identity.GetUserId();
            news.Author = db.Users.Find(news.AuthorId);
            var attachment = db.File.Where(f => f.URL.Contains(nvm.News.Attachment.URL)).FirstOrDefault();
            if (attachment != null)
            {
                news.AttachmentId = attachment.Id;
                news.Attachment = attachment;
            }
            else
                news.Attachment = null;
            db.News.Add(news);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(news);
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
            {
                var userid = User.Identity.GetUserId();
                news = db.News.Where(f => f.Id == id && f.AuthorId == userid).FirstOrDefault();
           } 
            if (news == null)
            {
                return HttpNotFound();
            }
            string type = news.Type.Type;
            NewsViewModel nvm = new NewsViewModel() { News = news, Type = type};
            return View(nvm);
        }

        // POST: /News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Users can only manage their own news unless they are admin
        [HttpPost]
        [Authorize(Roles = "Administrator,NewsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            var dbNews = db.News.Find(news.Id);
            if (roles.Select(r => r.Role.Name).Contains("Administrator") || dbNews.AuthorId == User.Identity.GetUserId())
            {
                //if (ModelState.IsValid)
                try
                {
                    dbNews.Body = news.Body;
                    dbNews.Title = news.Title;
                    dbNews.Tags = news.Tags;
                    db.Entry(dbNews).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(new NewsViewModel() { News = db.News.Find(news.Id) });
                }
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
