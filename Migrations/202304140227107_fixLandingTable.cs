namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixLandingTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Landings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Landings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(nullable: false, maxLength: 100),
                        LinkPicture = c.String(nullable: false, maxLength: 100),
                        Link = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
