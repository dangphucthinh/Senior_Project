namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class off_isPatient : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "isPatient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "isPatient", c => c.Boolean(nullable: false));
        }
    }
}
