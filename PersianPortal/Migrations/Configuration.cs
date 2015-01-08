namespace PersianPortal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PersianPortal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersianPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PersianPortal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<User>(
                new UserStore<User>(
                    new ApplicationDbContext()));
                        for (int i = 0; i < 4; i++)
                        {
                            var user = new User()
                            {
                                UserName = string.Format("User{0}", i.ToString()),
                                Name = "User",
                                FamilyName = i.ToString(),
                                DateOfBirth = DateTime.Now,
                                EmailAddress = string.Format("User{0}@gmail.com", i.ToString())
                            };
                            manager.Create(user, string.Format("Password{0}", i.ToString()));
                        }
        }
    }
}
