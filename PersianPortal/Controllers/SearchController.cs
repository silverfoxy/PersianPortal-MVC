using PersianPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersianPortal.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search/Details/5
        public ActionResult Details(string id)
        {
            SearchViewModel svm = new SearchViewModel();
            svm.Articles = svm.Articles.Where(a => a.Body.Contains(id) || a.Tags.Contains(id));
            svm.Books = svm.Books.Where(b => b.Name.Contains(id) || b.Tags.Contains(id) || b.Body.Contains(id) || b.Publisher.Contains(id));
            svm.Contents = svm.Contents.Where(c => c.Body.Contains(id) || c.Tags.Contains(id));
            svm.News = svm.News.Where(n => n.Body.Contains(id) || n.Title.Contains(id) || n.Tags.Contains(id));
            svm.Poems = svm.Poems.Where(p => p.Name.Contains(id) || p.Poet.Contains(id) || p.Tags.Contains(id) || p.Body.Contains(id));
            return View(svm);
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
