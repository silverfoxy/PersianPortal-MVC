using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class NewsViewModel
    {
        public News News { get; set; }

        public string Type { get; set; }
        public IEnumerable<NewsType> NewsTypes { get; set; }

        public NewsViewModel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            NewsTypes = db.NewsType;
        }
    }
}