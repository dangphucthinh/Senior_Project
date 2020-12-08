namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fk_all : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "DoctorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Appointments", "PatientId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Patients", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "DoctorId");
            CreateIndex("dbo.Appointments", "PatientId");
            CreateIndex("dbo.Appointments", "StatusId");
            CreateIndex("dbo.Doctors", "Specialty_Id");
            CreateIndex("dbo.Doctors", "Hospital_Id");
            CreateIndex("dbo.Specialties", "HsId");
            CreateIndex("dbo.Patients", "UserId");
            AddForeignKey("dbo.Appointments", "StatusId", "dbo.AppointmentStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Appointments", "DoctorId", "dbo.User", "Id");
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.User", "Id");
            AddForeignKey("dbo.Doctors", "Hospital_Id", "dbo.HospitalCenters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Specialties", "HsId", "dbo.HospitalSpecialties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Doctors", "Specialty_Id", "dbo.Specialties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Patients", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "UserId", "dbo.User");
            DropForeignKey("dbo.Doctors", "Specialty_Id", "dbo.Specialties");
            DropForeignKey("dbo.Specialties", "HsId", "dbo.HospitalSpecialties");
            DropForeignKey("dbo.Doctors", "Hospital_Id", "dbo.HospitalCenters");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.User");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.User");
            DropForeignKey("dbo.Appointments", "StatusId", "dbo.AppointmentStatus");
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.Specialties", new[] { "HsId" });
            DropIndex("dbo.Doctors", new[] { "Hospital_Id" });
            DropIndex("dbo.Doctors", new[] { "Specialty_Id" });
            DropIndex("dbo.Appointments", new[] { "StatusId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            AlterColumn("dbo.Patients", "UserId", c => c.String());
            AlterColumn("dbo.Appointments", "PatientId", c => c.String());
            AlterColumn("dbo.Appointments", "DoctorId", c => c.String());
        }
    }
}
