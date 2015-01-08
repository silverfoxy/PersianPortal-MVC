using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersianPortal.Models;

namespace PersianPortal.Controllers
{
    public class ContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contents
        public ActionResult Index()
        {
            var content = db.Content.Include(c => c.Author);
            return View(content.ToList());
        }

        // GET: Contents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Contents/Create
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,Tags,AuthorId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Content.Add(content);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", content.AuthorId);
            return View(content);
        }

        // GET: Contents/Edit/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", content.AuthorId);
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Tags,AuthorId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", content.AuthorId);
            return View(content);
        }

        // GET: Contents/Delete/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = db.Content.Find(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Content content = db.Content.Find(id);
            db.Content.Remove(content);
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
