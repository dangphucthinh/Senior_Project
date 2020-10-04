namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_HsId_Speciality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialties", "HsId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specialties", "HsId");
        }
    }
}
