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
    public class PoemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Poems
        public ActionResult Index()
        {
            var poem = db.Poem.Include(p => p.Author);
            return View(poem.ToList());
        }

        // GET: Poems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poem poem = db.Poem.Find(id);
            if (poem == null)
            {
                return HttpNotFound();
            }
            return View(poem);
        }

        // GET: Poems/Create
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Poems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,Tags,AuthorId,BookName,VoiceURL")] Poem poem)
        {
            if (ModelState.IsValid)
            {
                db.Poem.Add(poem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", poem.AuthorId);
            return View(poem);
        }

        // GET: Poems/Edit/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poem poem = db.Poem.Find(id);
            if (poem == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", poem.AuthorId);
            return View(poem);
        }

        // POST: Poems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Tags,AuthorId,BookName,VoiceURL")] Poem poem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", poem.AuthorId);
            return View(poem);
        }

        // GET: Poems/Delete/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poem poem = db.Poem.Find(id);
            if (poem == null)
            {
                return HttpNotFound();
            }
            return View(poem);
        }

        // POST: Poems/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poem poem = db.Poem.Find(id);
            db.Poem.Remove(poem);
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
