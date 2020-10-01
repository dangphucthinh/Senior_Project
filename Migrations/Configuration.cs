namespace Doctor_Appointment.Migrations
{
    using Doctor_Appointment.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Doctor_Appointment.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Doctor_Appointment.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Oscar",
                Email = "Oscar@enclave.vn",
                EmailConfirmed = true,
                FirstName = "Thinh",
                LastName = "Dang",
                DateOfBirth = DateTime.Now.AddYears(-3),
                Gender = true,
                Avatar = ""
            };

            manager.Create(user, "Admin@123");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = Constant.Constant.ADMIN });
                roleManager.Create(new IdentityRole { Name = Constant.Constant.DOCTOR });
                roleManager.Create(new IdentityRole { Name = Constant.Constant.PATIENT });
            }

            var adminUser = manager.FindByName("Oscar");

            manager.AddToRoles(adminUser.Id, new string[] { Constant.Constant.ADMIN, Constant.Constant.PATIENT });
        }
    }
}
