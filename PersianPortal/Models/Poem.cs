using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class Poem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "محتوا"), DataType(DataType.Html)]
        public string Body { get; set; }

        [MaxLength(300, ErrorMessage = "حداکثر طول مجاز برای تگ 300 کاراکتر است."), Display(Name = "تگ ها")]
        public string Tags { get; set; }

        public IEnumerable<File> Attachments { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [Required, Display(Name = "سبک")]
        public PoemType PoemType { get; set; }

        [MaxLength(50), Display(Name = "چاپ شده در کتاب")]
        public string BookName { get; set; }

        [MaxLength(200), Display(Name = "فایل صوتی"), DataType(DataType.Upload)]
        public string VoiceURL { get; set; }
    }

    public class PoemType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50), Display(Name = "سبک")]
        public string Type { get; set; }
    }
}