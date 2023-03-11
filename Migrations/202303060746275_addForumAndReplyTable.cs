namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForumAndReplyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 50),
                        Main = c.String(nullable: false),
                        InitDate = c.DateTime(),
                        ForumMemberId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumMembers", t => t.ForumMemberId)
                .Index(t => t.ForumMemberId);
            
            CreateTable(
                "dbo.ForumMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmedPassword = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        MembershipType = c.Int(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        IsCurrentMember = c.Boolean(),
                        Copy = c.String(maxLength: 100),
                        JobUnit = c.String(nullable: false, maxLength: 100),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        Education = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForumMemberExps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HistoryUnit = c.String(maxLength: 100),
                        HistoryJobTitle = c.String(maxLength: 100),
                        StartYear = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        ForumMemberId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumMembers", t => t.ForumMemberId)
                .Index(t => t.ForumMemberId);
            
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false, maxLength: 50),
                        Main = c.String(nullable: false),
                        InitDate = c.DateTime(),
                        ForumMemberId = c.Int(),
                        ForumId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fora", t => t.ForumId)
                .ForeignKey("dbo.ForumMembers", t => t.ForumMemberId)
                .Index(t => t.ForumMemberId)
                .Index(t => t.ForumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fora", "ForumMemberId", "dbo.ForumMembers");
            DropForeignKey("dbo.ForumReplies", "ForumMemberId", "dbo.ForumMembers");
            DropForeignKey("dbo.ForumReplies", "ForumId", "dbo.Fora");
            DropForeignKey("dbo.ForumMemberExps", "ForumMemberId", "dbo.ForumMembers");
            DropIndex("dbo.ForumReplies", new[] { "ForumId" });
            DropIndex("dbo.ForumReplies", new[] { "ForumMemberId" });
            DropIndex("dbo.ForumMemberExps", new[] { "ForumMemberId" });
            DropIndex("dbo.Fora", new[] { "ForumMemberId" });
            DropTable("dbo.ForumReplies");
            DropTable("dbo.ForumMemberExps");
            DropTable("dbo.ForumMembers");
            DropTable("dbo.Fora");
        }
    }
}
