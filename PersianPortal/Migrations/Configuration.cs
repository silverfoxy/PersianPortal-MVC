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

            var userManager = new UserManager<User>(
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
                            userManager.Create(user, string.Format("Password{0}", i.ToString()));
                        }

            //Creating Roles
            var roleManager = new RoleManager<Role>(new RoleStore<Role>(new ApplicationDbContext()));
            var adminRole = new Role("Administrator", "مدیر سایت");
            var newsAdminRole = new Role("NewsAdmin", "مدیر اخبار");
            var poemsAdminRole = new Role("PoemsAdmin", "مدیر اشعار");

            roleManager.Create<Role>(adminRole);
            roleManager.Create<Role>(newsAdminRole);
            roleManager.Create<Role>(poemsAdminRole);


            //Creating Admin Users
            var admin = new User() { UserName = "admin", DateOfBirth = DateTime.Parse("1371/01/01"), EmailAddress = "Admin@PersianPortal.ir", Name = "نینا", FamilyName = "رفیعی فر" };
            var newsAdmin = new User() { UserName = "NewsAdmin", DateOfBirth = DateTime.Parse("1371/01/01"), EmailAddress = "Admin@PersianPortal.ir", Name = "نینا", FamilyName = "رفیعی فر" };
            var poemsAdmin = new User() { UserName = "PoemsAdmin", DateOfBirth = DateTime.Parse("1371/01/01"), EmailAddress = "Admin@PersianPortal.ir", Name = "نینا", FamilyName = "رفیعی فر" };
            userManager.Create(admin, "123456");
            userManager.Create(poemsAdmin, "123456");
            userManager.Create(newsAdmin, "123456");

            //Assigning Roles To Users
            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });
            poemsAdmin.Roles.Add(new IdentityUserRole { RoleId = poemsAdminRole.Id, UserId = poemsAdmin.Id });
            newsAdmin.Roles.Add(new IdentityUserRole { RoleId = newsAdminRole.Id, UserId = newsAdmin.Id });
        }
    }
}
