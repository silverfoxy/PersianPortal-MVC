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
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
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
            var book = db.Book.Include(b => b.Author);
            return View(book.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                book.AuthorId = User.Identity.GetUserId();
                book.PublishDate = DateTime.Now;
                var attachment = db.File.Where(f => f.URL.Contains(book.PDF.URL)).FirstOrDefault();
                if (attachment != null)
                {
                    if (attachment.Extension != FileExtensions.pdf)
                    {
                        return View(book);
                    }
                    book.PDF = attachment;
                }
                else
                    book.PDF = null;
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", book.AuthorId);
                return View(book);
            }
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                book = db.Book.Find(id);
            else
                book = db.Book.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            var dbBook = db.Book.Find(book.Id);
            if (roles.Select(r => r.Role.Name).Contains("Administrator") || dbBook.AuthorId == User.Identity.GetUserId())
            {
                //if (ModelState.IsValid)
                try
                {
                    dbBook.Body = book.Body;
                    dbBook.Tags = book.Tags;
                    dbBook.Publisher = book.Publisher;
                    dbBook.Version = book.Version;
                    dbBook.Name = book.Name;
                    db.Entry(dbBook).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(db.Poem.Find(dbBook.Id));
                }
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", dbBook.AuthorId);
            return View(dbBook);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                book = db.Book.Find(id);
            else
                book = db.Book.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                book = db.Book.Find(id);
            else
                book = db.Book.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
            db.Book.Remove(book);
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
