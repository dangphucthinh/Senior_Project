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
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Foreign Neurology" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Foreign Injury" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Chest Surgery" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Cardiovascular intervention" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "External digestion" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Extra urinary" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Cardiomyopathy" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Endocrine - Hepatobiliary" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Internal respiratory system" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Endocrine" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Neurology - musculoskeletal - blood transfusion" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Active resuscitation against poison" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Anesthesia Resuscitation" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Tumor" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Ear, nose and throat" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Dentomaxillofacial" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Foreign burns" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Foreign synthesis" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Internal synthesis" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Tropical medicine" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Nuclear medicine" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Rehabilitation" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Maternity" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Artificial kidney" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Eyes" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Geriatrics" });
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Department of Nutrition" });

            context.hospitalSpecialties.AddRange(hospitalSpecialties);
            context.SaveChanges();

            //Create list of Speacialties
            List<Specialty> specialties = new List<Specialty>();
            specialties.Add(new Specialty { Name = "Pediatrics", HsId = 1 });
            specialties.Add(new Specialty { Name = "Vaccine", HsId = 1 });
            specialties.Add(new Specialty { Name = "Cardiology", HsId = 2 });
            specialties.Add(new Specialty { Name = "Radiology", HsId = 3 });
            specialties.Add(new Specialty { Name = "Oncology", HsId = 4 });
            specialties.Add(new Specialty { Name = "Pharmacy", HsId = 5 });
            specialties.Add(new Specialty { Name = "Orthopedic", HsId = 6 });
            specialties.Add(new Specialty { Name = "Orthopedic surgery", HsId = 6 });
            specialties.Add(new Specialty { Name = "Lab test", HsId = 7 });
            specialties.Add(new Specialty { Name = "Resuscitation & Emergency", HsId = 8 });
            specialties.Add(new Specialty { Name = "Gynecology & Obstetrics", HsId = 9 });
            specialties.Add(new Specialty { Name = "Gynecology", HsId = 9 });
            specialties.Add(new Specialty { Name = "Anesthetics & Recovery", HsId = 10 });
            specialties.Add(new Specialty { Name = "General examination", HsId = 11 });
            specialties.Add(new Specialty { Name = "General Internal Medicine", HsId = 11 });

            context.specialties.AddRange(specialties);
            context.SaveChanges();


            ////create list address
            //List<Address> addresses = new List<Address>();
            //addresses.Add(new Address
            //{
            //    AddressNumber = 102,
            //    Street = "Nguyen Nhan,Hoa Tho Dong, Cam Le",
            //    City = "Da Nang",
            //});
            //addresses.Add(new Address
            //{
            //    AddressNumber = 64,
            //    Street = "Cach Mang Thang Tam, Khue Trung, Cam Le",
            //    City = "Da Nang",
            //});
            //addresses.Add(new Address
            //{
            //    AddressNumber = 9,
            //    Street = "Tran Thu Do, Khue Trung, Cam Le ",
            //    City = "Da Nang",
            //});
            //addresses.Add(new Address
            //{
            //    AddressNumber = 64,
            //    Street = "Phan Dang Luu, Hoa Cuong Bac, Hai Chau",
            //    City = "Da Nang",
            //});
            //addresses.Add(new Address
            //{
            //    AddressNumber = 219,
            //    Street = "Nguyen Van Linh, Thac Gian, Cam Le",
            //    City = "Da Nang",
            //});
            //context.addresses.AddRange(addresses);
            //context.SaveChanges();


            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1604048938/e1p2tjwsp4zydfjhtg4z.jpg do anh vu
            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1604240896/um8s8bnx07vhdlsimk6u.jpg doan vu ngoc lam
        }
    }
}

