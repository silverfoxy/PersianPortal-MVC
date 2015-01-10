using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Article> Articles { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<Content> Contents { get; set; }

        public IEnumerable<News> News { get; set; }

        public IEnumerable<Poem> Poems { get; set; }

    }
}