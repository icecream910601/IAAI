namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewsCatalogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsCatalogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Class = c.String(nullable: false, maxLength: 50),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.News", "ClassId", c => c.Int());
            AlterColumn("dbo.News", "Picture", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.News", "Subject", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.News", "Content", c => c.String(nullable: false, maxLength: 1000));
            CreateIndex("dbo.News", "ClassId");
            AddForeignKey("dbo.News", "ClassId", "dbo.NewsCatalogs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "ClassId", "dbo.NewsCatalogs");
            DropIndex("dbo.News", new[] { "ClassId" });
            AlterColumn("dbo.News", "Content", c => c.String(maxLength: 1000));
            AlterColumn("dbo.News", "Subject", c => c.String(maxLength: 200));
            AlterColumn("dbo.News", "Picture", c => c.String(maxLength: 200));
            DropColumn("dbo.News", "ClassId");
            DropTable("dbo.NewsCatalogs");
        }
    }
}
