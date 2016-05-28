namespace NetPress.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NetPress.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NetPress.Models.ApplicationDbContext context)
        {
            //CREATE ROLES
            context.Roles.AddOrUpdate(r => r.Name,
               new IdentityRole { Name = "Admin" },
               new IdentityRole { Name = "Author" }
               );

            //CREATE DEAFULT ADMIN
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
          //  var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string email = "admin@netpress.com";
            string name = "Boss";
            string surname = "Ross";
            string password = "likeabaws";

            var adminUser = new ApplicationUser {
                UserName = email, Email = email, Name = name, Surname = surname,
                MemberSince = DateTime.Now };


            var result = UserManager.Create(adminUser, password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(adminUser.Id, "Admin");
              
            }


        }
    }
}
