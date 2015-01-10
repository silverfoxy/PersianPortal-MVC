using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersianPortal.Models;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace PersianPortal.Controllers
{
    [Authorize(Roles = "Administrator,PoemsAdmin,NewsAdmin")]
    public class FilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Files
        //Users can only view their own files unless they are admin
        public ActionResult Index()
        {
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                return View(db.File.ToList());
            else
            {
                var userId = User.Identity.GetUserId();
                return View(db.File.Where(f => f.UploaderId == userId).ToList());
            }
        }

        // GET: Files/Details/5
        //Users can only view their own files unless they are admin
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                file = db.File.Find(id);
            else
            {
                var usrid = User.Identity.GetUserId();
                file = db.File.Where(f => f.Id == id && f.UploaderId == usrid).FirstOrDefault();
            }
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Upload
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Files/Upload
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Upload")]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile()
        {
            string filePath = "";
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        //extension.Substring(1) to remove the '.'
                        var extension = Path.GetExtension(file.FileName).Substring(1).ToLower();
                        filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        if (!Enum.GetNames(typeof(FileExtensions)).Contains(extension))
                            throw new Exception("Invalid FileType.");
                        else
                        {
                            file.SaveAs(filePath);
                            var dbFile = new Models.File() { UploaderId = User.Identity.GetUserId(), Extension = (FileExtensions)Enum.Parse(typeof(FileExtensions), extension), URL = "/Uploads/" + fileName };
                            db.File.Add(dbFile);
                            db.SaveChanges();
                            return RedirectToAction("Details", new { id = dbFile.Id });
                        }
                    }
                    else
                        throw new Exception("No File Was Submitted.");
                }
                else
                    throw new Exception("No File Was Submitted.");
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(filePath);
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = db.File.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Extension,URL")] Models.File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        //Users can only delete their own files unless they are admin
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                file = db.File.Find(id);
            else
                file = db.File.Where(f => f.Id == id && f.UploaderId == User.Identity.GetUserId()).FirstOrDefault();
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        //Users can only delete their own files unless they are admin
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.File file;
            var roles = db.Users.Find(User.Identity.GetUserId()).Roles.ToList();
            if (roles.Select(r => r.Role.Name).Contains("Administrator"))
                file = db.File.Find(id);
            else
                file = db.File.Where(f => f.Id == id && f.UploaderId == User.Identity.GetUserId()).FirstOrDefault();
            db.File.Remove(file);
            db.SaveChanges();
            System.IO.File.Delete(Path.Combine(Server.MapPath(string.Format("~/{0}", file.URL))));
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
