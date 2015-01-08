using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PersianPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای نام 50 کاراکتر است."), Display(Name = "نام")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز برای نام خانوادگی 200 کاراکتر است."), Display(Name = "نام خانوادگی")]
        public string FamilyName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date), Display(Name = "تاریخ تولد")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress, Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
    }

    public class Role : IdentityRole
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public Role() : base() { }

        public Role(string name, string title) : base(name)
        {
            this.Title = title;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Article { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsType { get; set; }
        public DbSet<Poem> Poem { get; set; }
        public DbSet<PoemType> PoemType { get; set; }

        public DbSet<Content> Content { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

    }
}