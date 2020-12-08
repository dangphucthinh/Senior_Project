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
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Foreign Neurology", HosSpecImg = "http://res.cloudinary.com/deh0sqxwl/image/upload/v1607408356/aeuign0v53epn7zdyo90.png" }); // khoa than kinh
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Gastroenterology" }); // khoa tiêu hoá
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Pediatrics" }); //khoa nhi
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Obstetrics" }); //khoa sản
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Neurology - musculoskeletal - blood transfusion" }); // noi than kinh- co xuong khop
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Cardiology" }); //khoa tim mach
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Nephrology - Endocrinology" }); //khoa thận - nội tiết
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Dermatology" }); // khoa da liễu
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "General medicine" }); // đa khoa
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Oncolgy & Hematology " }); // ung bướu
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Nutrition" }); // khoa dinh dưỡng
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Internal espiration" }); // nội hô hấp 
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Geriatrics" }); //lão khoa
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Blood vessel" }); //ngoại mạch máu
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Eyes" }); // khoa mắt, nhãn khoa
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "General pediatric" }); // nội tổng hợp
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Ear, nose and throat" }); // tai- mũi- họng
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Physiotherapy" }); // vật lý trị liệu
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Traditional medicine" }); // y học cổ truyền
            hospitalSpecialties.Add(new HospitalSpecialty { Name = "Infectious Diseases" }); // khoa truyen nhiem

            context.hospitalSpecialties.AddRange(hospitalSpecialties);
            context.SaveChanges();


            //create list hospital center
            List<HospitalCenter> hospitalCenters = new List<HospitalCenter>();
            hospitalCenters.Add(new HospitalCenter { Name = "Cam Le General Hospital", Address = " 2 Tran Quy Hai, Hoa Tho Dong, Cam Le, Da Nang" });
            hospitalCenters.Add(new HospitalCenter { Name = "Tam Tri General Hospital", Address = "64 Cach Mang Thang Tam, Khue Trung, Cam Le, Da Nang" });
            hospitalCenters.Add(new HospitalCenter { Name = "Traditional Medicine Hospital", Address = "9 Tran Thu Do, Khue Trung, Cam Le, Da Nang" });
            hospitalCenters.Add(new HospitalCenter { Name = "Da Nang Eye - hospital", Address = "68 Phan Dang Luu, Hai Chau, Da Nang" });
            hospitalCenters.Add(new HospitalCenter { Name = "Hoan My General Hospital", Address = "Nguyen Van Linh, Thac Giac, Thanh Khe, Da Nang" });

            context.hospitalCenters.AddRange(hospitalCenters);
            context.SaveChanges();

            //Create list of Speacialties
            List<Specialty> specialties = new List<Specialty>();
            specialties.Add(new Specialty { Name = "Foreign Neurology", HsId = 1 });
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

            //

            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1604048938/e1p2tjwsp4zydfjhtg4z.jpg do anh vu
            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1604240896/um8s8bnx07vhdlsimk6u.jpg doan vu ngoc lam
            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1607178308/loxavfpuujkjlhgfedy5.jpg nguyen tat binh
            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1607178387/t8xfnhvklydynlw8j6nq.jpg vo thanh nhan
            //http://res.cloudinary.com/deh0sqxwl/image/upload/v1607178539/mo2lggrrfjlsdasz6tft.png khong trong thang
        }
    }
}

