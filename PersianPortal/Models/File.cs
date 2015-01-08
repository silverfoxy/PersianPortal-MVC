using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersianPortal.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "پسوند فایل")]
        public Extension Extension { get; set; }

        [Required]
        [MaxLength(200), Display(Name = "آدرس فایل"), DataType(DataType.Upload)]
        public string URL { get; set; }

        [Required]
        public string UploaderId { get; set; }

        [ForeignKey("UploaderId")]
        public virtual User UploaderUser { get; set; }
    }

    public enum Extension
    {
        pdf,
        jpg,
        png,
        gif,
        mp3,
        wav,
        wma,
        flv
    };
}