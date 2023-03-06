namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequiredTag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CertifiedMembers", "Picture", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CertifiedMembers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "Country", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CertifiedMembers", "Company", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CertifiedMembers", "Company", c => c.String(maxLength: 100));
            AlterColumn("dbo.CertifiedMembers", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.CertifiedMembers", "Country", c => c.String(maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.CertifiedMembers", "Picture", c => c.String(maxLength: 100));
        }
    }
}
