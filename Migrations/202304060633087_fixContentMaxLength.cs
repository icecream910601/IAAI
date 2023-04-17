namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixContentMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Knowledges", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.News", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Content", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Knowledges", "Content", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
