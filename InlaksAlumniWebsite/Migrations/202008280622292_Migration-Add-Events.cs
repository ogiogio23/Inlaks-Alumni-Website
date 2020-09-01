namespace InlaksAlumniWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAddEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(nullable: false),
                        EventLocation = c.String(nullable: false),
                        EventDate = c.String(),
                        EventTime = c.String(),
                        Description = c.String(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
