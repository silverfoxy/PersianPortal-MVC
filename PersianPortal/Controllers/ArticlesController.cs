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
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
                if (roles.Select(r => r.Role.Name).Contains("Administrator") || roles.Select(r => r.Role.Name).Contains("PoemsAdmin"))
                {
                    ViewBag.CanViewNewsPanel = true;
                }
            }
            else
                ViewBag.CanViewNewsPanel = false;
            var article = db.Article.Include(a => a.Author);
            return View(article.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            try
            {
                article.AuthorId = User.Identity.GetUserId();
                article.PublishDate = DateTime.Now;
                var attachment = db.File.Where(f => f.URL.Contains(article.PDF.URL)).FirstOrDefault();
                if (attachment != null)
                {
                    if (attachment.Extension != FileExtensions.pdf)
                    {
                        return View(article);
                    }
                    article.PDF = attachment;
                }
                else
                    article.PDF = null;
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", article.AuthorId);
                return View(article);
            }
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                article = db.Article.Find(id);
            else
                article = db.Article.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", article.AuthorId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            var dbArticle = db.Article.Find(article.Id);
            if (roles.Select(r => r.Role.Name).Contains("Administrator") || dbArticle.AuthorId == User.Identity.GetUserId())
            {
                //if (ModelState.IsValid)
                try
                {
                    dbArticle.Body = article.Body;
                    dbArticle.Tags = article.Tags;
                    dbArticle.Magazine = article.Magazine;
                    dbArticle.Title = article.Title;
                    db.Entry(dbArticle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(db.Poem.Find(dbArticle.Id));
                }
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                article = db.Article.Find(id);
            else
                article = db.Article.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                article = db.Article.Find(id);
            else
                article = db.Article.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            db.Article.Remove(article);
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
