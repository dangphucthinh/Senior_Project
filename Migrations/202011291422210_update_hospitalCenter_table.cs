namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_hospitalCenter_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HospitalCenters", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HospitalCenters", "Address");
        }
    }
}
