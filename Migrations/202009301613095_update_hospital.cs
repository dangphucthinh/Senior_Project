namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_hospital : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HospitalCenters", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HospitalCenters", "Name");
        }
    }
}
