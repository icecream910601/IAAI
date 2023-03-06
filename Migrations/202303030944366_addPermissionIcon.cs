namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPermissionIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "Icon", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "Icon");
        }
    }
}
