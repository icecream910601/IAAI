namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixLandingPageContentTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LandingPageContents", "BarText");
            DropColumn("dbo.LandingPageContents", "BarPic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LandingPageContents", "BarPic", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.LandingPageContents", "BarText", c => c.String(nullable: false));
        }
    }
}
