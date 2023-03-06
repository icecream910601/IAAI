namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPermissionController : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "Controller", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "Controller");
        }
    }
}
