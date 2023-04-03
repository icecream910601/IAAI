namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAssnTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssnBusinesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Business = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssnLicenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Licenses = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssnRefers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Refer = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssnSurveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Survey = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AssnSurveys");
            DropTable("dbo.AssnRefers");
            DropTable("dbo.AssnLicenses");
            DropTable("dbo.AssnBusinesses");
        }
    }
}
