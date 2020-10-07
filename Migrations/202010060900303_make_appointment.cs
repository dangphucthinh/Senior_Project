namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class make_appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        MeetingTime = c.DateTime(nullable: false),
                        Issue = c.String(),
                        AppointmentType = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppointmentStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppointmentStatus");
            DropTable("dbo.Appointments");
        }
    }
}
