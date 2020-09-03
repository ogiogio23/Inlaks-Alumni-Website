namespace InlaksAlumniWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAddAlumnis : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumni",
                c => new
                    {
                        AlumniId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PastEmployeeId = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        DateOfEmployment = c.String(),
                        PositionHeld = c.String(nullable: false),
                        DateLeft = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Gender = c.String(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.AlumniId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationId = c.Int(nullable: false, identity: true),
                        TransactionRef = c.String(nullable: false),
                        AmountPaid = c.Single(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        AlumniId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationId)
                .ForeignKey("dbo.Alumni", t => t.AlumniId, cascadeDelete: true)
                .Index(t => t.AlumniId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "AlumniId", "dbo.Alumni");
            DropIndex("dbo.Donations", new[] { "AlumniId" });
            DropTable("dbo.Donations");
            DropTable("dbo.Alumni");
        }
    }
}
