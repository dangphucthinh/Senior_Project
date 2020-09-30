namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address_Number = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Certification = c.String(),
                        Education = c.String(),
                        Specialty_Id = c.Int(nullable: false),
                        Hospital_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HospitalCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address_Number = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        Address_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Medical_History = c.String(),
                        Sympton = c.String(),
                        Allergy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "Address_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Address_Id");
            DropTable("dbo.Specialties");
            DropTable("dbo.Patients");
            DropTable("dbo.HospitalCenters");
            DropTable("dbo.Doctors");
            DropTable("dbo.Addresses");
        }
    }
}
