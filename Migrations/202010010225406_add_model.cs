namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_model : DbMigration
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
                        Name = c.String(),
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
            
            AddColumn("dbo.User", "isPatient", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "Address_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.User", "LastName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.User", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.User", "Address_Id");
            DropColumn("dbo.User", "isPatient");
            DropTable("dbo.Specialties");
            DropTable("dbo.Patients");
            DropTable("dbo.HospitalCenters");
            DropTable("dbo.Doctors");
            DropTable("dbo.Addresses");
        }
    }
}
