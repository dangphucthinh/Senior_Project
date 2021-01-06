namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Address");
        }
    }
}
