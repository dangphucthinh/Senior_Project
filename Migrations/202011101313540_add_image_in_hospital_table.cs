namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_image_in_hospital_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HospitalCenters", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HospitalCenters", "Image");
        }
    }
}
