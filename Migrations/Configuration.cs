namespace Doctor_Appointment.Migrations
{
    using Doctor_Appointment.Infrastucture;
    using Doctor_Appointment.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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

            
            //Create list of Hospital Specialties
            List<HospitalSpecialty> hospitalSpecialties = new List<HospitalSpecialty>();
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Pediatrics – Neonatology" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Cardiovascular Center" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Diagnostic imaging Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Oncology Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Pharmacy" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "General Surgical Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Laboratory Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Intensive Care and Emergency Medicine" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Obstetrics Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Anesthesiology Department" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "General Internal Medicine" });

            context.hospitalSpecialties.AddRange(hospitalSpecialties);
            context.SaveChanges();

            //Create list of Speacialties
            List<Specialty> specialties = new List<Specialty>();
            specialties.Add(new Specialty { Name = "Pediatrics", HsId = 1});
            specialties.Add(new Specialty { Name = "Vaccine", HsId = 1});
            specialties.Add(new Specialty { Name = "Cardiology", HsId = 2});
            specialties.Add(new Specialty { Name = "Radiology", HsId = 3});
            specialties.Add(new Specialty { Name = "Oncology", HsId = 4});
            specialties.Add(new Specialty { Name = "Pharmacy", HsId = 5});
            specialties.Add(new Specialty { Name = "Orthopedic", HsId = 6});
            specialties.Add(new Specialty { Name = "Orthopedic surgery", HsId = 6});
            specialties.Add(new Specialty { Name = "Lab test", HsId = 7});
            specialties.Add(new Specialty { Name = "Resuscitation & Emergency", HsId = 8});
            specialties.Add(new Specialty { Name = "Gynecology & Obstetrics", HsId = 9});
            specialties.Add(new Specialty { Name = "Gynecology", HsId = 9});
            specialties.Add(new Specialty { Name = "Anesthetics & Recovery", HsId = 10});
            specialties.Add(new Specialty { Name = "General examination", HsId = 11});
            specialties.Add(new Specialty { Name = "General Internal Medicine", HsId = 11});

            context.specialties.AddRange(specialties);
            context.SaveChanges();
        }
    }
}
