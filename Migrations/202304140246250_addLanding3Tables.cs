namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLanding3Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandingPageContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarText = c.String(nullable: false),
                        BarPic = c.String(nullable: false, maxLength: 100),
                        Picture1 = c.String(nullable: false, maxLength: 100),
                        Picture2 = c.String(nullable: false, maxLength: 100),
                        Picture3 = c.String(nullable: false, maxLength: 100),
                        Picture4 = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandingPageLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkPicture = c.String(nullable: false, maxLength: 100),
                        Link = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LandingPageSliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SliderPicture = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LandingPageSliders");
            DropTable("dbo.LandingPageLinks");
            DropTable("dbo.LandingPageContents");
        }
    }
}
