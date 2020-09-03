namespace InlaksAlumniWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAddDonations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "DonationTitle", c => c.String(nullable: false));
            AddColumn("dbo.Donations", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.Donations", "DonationType", c => c.String(nullable: false));
            DropColumn("dbo.Donations", "TransactionRef");
            DropColumn("dbo.Donations", "AmountPaid");
            DropColumn("dbo.Donations", "PaymentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Donations", "PaymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Donations", "AmountPaid", c => c.Single(nullable: false));
            AddColumn("dbo.Donations", "TransactionRef", c => c.String(nullable: false));
            DropColumn("dbo.Donations", "DonationType");
            DropColumn("dbo.Donations", "Amount");
            DropColumn("dbo.Donations", "DonationTitle");
        }
    }
}
