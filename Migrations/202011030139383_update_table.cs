namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "DoctorId", c => c.String());
            AlterColumn("dbo.Appointments", "PatientId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "PatientId", c => c.Int(nullable: false));
            AlterColumn("dbo.Appointments", "DoctorId", c => c.Int(nullable: false));
        }
    }
}
