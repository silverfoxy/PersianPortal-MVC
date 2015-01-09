using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersianPortal.Models
{
    public class Poem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "نام شعر")]
        [MaxLength(100, ErrorMessage = "نام شعر حداکثر می تواند 100 کاراکتر باشد.")]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "متن شعر"), DataType(DataType.Html)]
        public string Body { get; set; }

        [MaxLength(300, ErrorMessage = "حداکثر طول مجاز برای تگ 300 کاراکتر است."), Display(Name = "تگ ها")]
        public string Tags { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [Required]
        [Display(Name = "شاعر")]
        [MaxLength(100, ErrorMessage = "نام شاعر حداکثر می تواند 100 کاراکتر باشد.")]
        public string Poet { get; set; }

        [Required]
        public int PoemTypeId { get; set; }

        [ForeignKey("PoemTypeId")]
        [Display(Name = "سبک")]
        public virtual PoemType PoemType { get; set; }

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