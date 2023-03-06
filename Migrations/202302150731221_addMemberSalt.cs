namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMemberSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Salt", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Salt");
        }
    }
}
