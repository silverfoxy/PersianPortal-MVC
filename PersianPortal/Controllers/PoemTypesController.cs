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
    [Authorize(Roles = "Administrator")]
    public class PoemTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoemTypes
        public ActionResult Index()
        {
            return View(db.PoemType.ToList());
        }

        // GET: PoemTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoemType poemType = db.PoemType.Find(id);
            if (poemType == null)
            {
                return HttpNotFound();
            }
            return View(poemType);
        }

        // GET: PoemTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoemTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] PoemType poemType)
        {
            if (ModelState.IsValid)
            {
                db.PoemType.Add(poemType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poemType);
        }

        // GET: PoemTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoemType poemType = db.PoemType.Find(id);
            if (poemType == null)
            {
                return HttpNotFound();
            }
            return View(poemType);
        }

        // POST: PoemTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] PoemType poemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poemType);
        }

        // GET: PoemTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoemType poemType = db.PoemType.Find(id);
            if (poemType == null)
            {
                return HttpNotFound();
            }
            return View(poemType);
        }

        // POST: PoemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoemType poemType = db.PoemType.Find(id);
            db.PoemType.Remove(poemType);
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
