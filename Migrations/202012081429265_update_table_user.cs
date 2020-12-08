namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_user : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.User", "Address");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.User", "Address", c => c.String());
        }
    }
}
