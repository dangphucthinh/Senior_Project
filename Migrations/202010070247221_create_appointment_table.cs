namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_appointment_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "UserId", c => c.String());
            DropColumn("dbo.Patients", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "User_Id", c => c.String());
            DropColumn("dbo.Patients", "UserId");
        }
    }
}
