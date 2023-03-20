namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixForumMemberExpTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumMemberExps", "StartYear", c => c.Int());
            AlterColumn("dbo.ForumMemberExps", "StartMonth", c => c.Int());
            AlterColumn("dbo.ForumMemberExps", "EndYear", c => c.Int());
            AlterColumn("dbo.ForumMemberExps", "EndMonth", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ForumMemberExps", "EndMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.ForumMemberExps", "EndYear", c => c.Int(nullable: false));
            AlterColumn("dbo.ForumMemberExps", "StartMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.ForumMemberExps", "StartYear", c => c.Int(nullable: false));
        }
    }
}
