namespace Doctor_Appointment.Migrations
{
    using Doctor_Appointment.Infrastucture;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Doctor_Appointment.Infrastucture.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Doctor_Appointment.Infrastucture.ApplicationDbContext context)
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
                Gender = true,
                DateOfBirth = DateTime.Now.AddYears(-3),
                isPatient = true
            };

            manager.Create(user, "Admin@123");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Doctor" });
                roleManager.Create(new IdentityRole { Name = "Patient" });
            }

            var adminUser = manager.FindByName("Oscar");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
        }
    }    
}
