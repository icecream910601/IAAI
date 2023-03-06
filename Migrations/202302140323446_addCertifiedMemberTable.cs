namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCertifiedMemberTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertifiedMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        Title = c.String(maxLength: 100),
                        Company = c.String(maxLength: 100),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CertifiedMembers");
        }
    }
}
