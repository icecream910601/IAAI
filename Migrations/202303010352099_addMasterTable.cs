namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMasterTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(maxLength: 100),
                        Name = c.String(maxLength: 50),
                        PresentJob = c.String(maxLength: 100),
                        Education = c.String(maxLength: 100),
                        Introduction = c.String(maxLength: 500),
                        Experience = c.String(maxLength: 500),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Masters");
        }
    }
}
