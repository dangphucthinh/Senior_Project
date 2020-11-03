namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_appointment_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "StartTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "StartTime");
        }
    }
}
