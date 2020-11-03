namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTableDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Bio");
        }
    }
}
