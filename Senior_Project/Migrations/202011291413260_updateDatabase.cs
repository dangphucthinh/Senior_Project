namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HospitalCenters", "Address_Id");
            DropColumn("dbo.User", "Address_Id");
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressNumber = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "Address_Id", c => c.Int(nullable: false));
            AddColumn("dbo.HospitalCenters", "Address_Id", c => c.Int(nullable: false));
        }
    }
}
