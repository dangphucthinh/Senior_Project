using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Infrastucture

{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //AspNetUsers -> User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");

            //AspNetUsers -> Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");

            //AspNetUsers -> UserRole
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");

            //AspNetUsers -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");

            //AspNetUsers -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");
        }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<HospitalCenter> hospitalCenters { get; set; }
        public DbSet<Specialty> specialties { get; set; }
        public DbSet<HospitalSpecialty> hospitalSpecialties { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<AppointmentStatus> appointmentStatuses { get; set; }
    }
}