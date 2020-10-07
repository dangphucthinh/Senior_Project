namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_appointment_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Detail", c => c.String());
            DropColumn("dbo.Appointments", "AppointmentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "AppointmentType", c => c.Int(nullable: false));
            DropColumn("dbo.Appointments", "Detail");
        }
    }
}
