namespace InlaksAlumniWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAddEventImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventImages",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImagesUrl = c.String(),
                        EventId = c.Int(nullable: false),
                        eventImage_ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.EventImages", t => t.eventImage_ImageId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.eventImage_ImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventImages", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventImages", "eventImage_ImageId", "dbo.EventImages");
            DropIndex("dbo.EventImages", new[] { "eventImage_ImageId" });
            DropIndex("dbo.EventImages", new[] { "EventId" });
            DropTable("dbo.EventImages");
        }
    }
}
