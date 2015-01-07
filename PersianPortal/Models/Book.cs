using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "محتوا")]
        public string Body { get; set; }

        [MaxLength(300, ErrorMessage = "حداکثر طول مجاز برای تگ 300 کاراکتر است."), Display(Name = "تگ ها")]
        public string Tags { get; set; }

        [MaxLength(200)]
        public IEnumerable<string> Attachments { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [DataType(DataType.Date), Display(Name = "تاریخ چاپ")]
        public DateTime PublishDate { get; set; }

        public int Version { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام کتاب می تواند 100 کاراکتر باشد.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام ناشر می تواند 100 کاراکتر باشد.")]
        public string Publisher { get; set; }

        [MaxLength(200)]
        public string PDFURL { get; set; }
    }
}