using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class SearchViewModel
    {
        public IQueryable<Article> Articles { get; set; }

        public IQueryable<Book> Books { get; set; }

        public IQueryable<Content> Contents { get; set; }

        public IQueryable<News> News { get; set; }

        public IQueryable<Poem> Poems { get; set; }

        public SearchViewModel()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Articles = db.Article;
                Books = db.Book;
                Contents = db.Content;
                News = db.News;
                Poems = db.Poem;
            }
        }
    }
}