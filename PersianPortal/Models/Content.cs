using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersianPortal.Models
{
    public class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "عنوان"), MaxLength(200, ErrorMessage = "حداکثر طول عنوان باید 200 کاراکتر باشد.")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "محتوا"), DataType(DataType.Html)]
        public string Body { get; set; }

        [MaxLength(300, ErrorMessage = "حداکثر طول مجاز برای تگ 300 کاراکتر است."), Display(Name = "تگ ها")]
        public string Tags { get; set; }

        public int? AttachmentId { get; set; }

        [Display(Name = "پیوند ها")]
        [ForeignKey("AttachmentId")]
        public virtual File Attachment { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }
    }
}