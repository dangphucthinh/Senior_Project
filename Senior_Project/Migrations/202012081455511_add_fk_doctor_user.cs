namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fk_doctor_user : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Doctors", "UserId");
            AddForeignKey("dbo.Doctors", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "UserId", "dbo.User");
            DropIndex("dbo.Doctors", new[] { "UserId" });
            AlterColumn("dbo.Doctors", "UserId", c => c.String());
        }
    }
}
