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
    public class PoemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Poems
        public ActionResult Index()
        {
            var poem = db.Poem.Include(p => p.PoemType);
            return View(db.Poem.ToList());
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
            ViewBag.PoemTypes = new SelectList(db.PoemType, "Id", "Type");
            return View();
        }

        // POST: Poems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Poem poem)
        {
            if (ModelState.IsValid)
            {
                poem.PoemTypeId = poem.PoemType.Id;
                poem.PoemType = db.PoemType.Find(poem.PoemType.Id);
                poem.AuthorId = User.Identity.GetUserId();
                db.Poem.Add(poem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", poem.AuthorId);
            ViewBag.PoemTypes = new SelectList(db.PoemType, "Id", "Type");
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
            Poem poem;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                poem = db.Poem.Find(id);
            else
                poem = db.Poem.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
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
        public ActionResult Edit(Poem poem)
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            var dbPoem = db.Poem.Find(poem.Id);
            if (roles.Select(r => r.Role.Name).Contains("Administrator") || dbPoem.AuthorId == User.Identity.GetUserId())
            {
                //if (ModelState.IsValid)
                try
                {
                    dbPoem.Body = poem.Body;
                    dbPoem.Name = poem.Name;
                    dbPoem.Tags = poem.Tags;
                    dbPoem.Poet = poem.Poet;
                    db.Entry(dbPoem).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(db.Poem.Find(poem.Id));
                }
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", poem.AuthorId);
            ViewBag.PoemTypes = new SelectList(db.PoemType, "Id", "Type");
            return View();
        }

        // GET: Poems/Delete/5
        [Authorize(Roles = "Administrator,PoemsAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poem poem;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                poem = db.Poem.Find(id);
            else
                poem = db.Poem.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
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
            Poem poem;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                poem = db.Poem.Find(id);
            else
                poem = db.Poem.Where(f => f.Id == id && f.AuthorId == User.Identity.GetUserId()).FirstOrDefault();
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
