namespace Doctor_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_speciality : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Specialties", "HsId", "dbo.HospitalSpecialties");
            DropForeignKey("dbo.Doctors", "Specialty_Id", "dbo.Specialties");
            DropIndex("dbo.Doctors", new[] { "Specialty_Id" });
            DropIndex("dbo.Specialties", new[] { "HsId" });
            CreateIndex("dbo.Doctors", "HospitalSpecialty_Id");
            AddForeignKey("dbo.Doctors", "HospitalSpecialty_Id", "dbo.HospitalSpecialties", "Id", cascadeDelete: true);
            DropColumn("dbo.Doctors", "Specialty_Id");
            DropTable("dbo.Specialties");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        HsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "Specialty_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Doctors", "HospitalSpecialty_Id", "dbo.HospitalSpecialties");
            DropIndex("dbo.Doctors", new[] { "HospitalSpecialty_Id" });
            CreateIndex("dbo.Specialties", "HsId");
            CreateIndex("dbo.Doctors", "Specialty_Id");
            AddForeignKey("dbo.Doctors", "Specialty_Id", "dbo.Specialties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Specialties", "HsId", "dbo.HospitalSpecialties", "Id", cascadeDelete: true);
        }
    }
}
