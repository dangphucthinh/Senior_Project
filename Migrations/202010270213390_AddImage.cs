namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HospitalSpecialties", "HosSpecImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HospitalSpecialties", "HosSpecImg");
        }
    }
}
