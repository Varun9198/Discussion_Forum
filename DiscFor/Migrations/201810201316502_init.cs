namespace DiscFor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        AnswerString = c.String(),
                        CurrentQuestion_QuestionId = c.Int(),
                        CurrentUser_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.CurrentQuestion_QuestionId)
                .ForeignKey("dbo.Users", t => t.CurrentUser_UserId)
                .Index(t => t.CurrentQuestion_QuestionId)
                .Index(t => t.CurrentUser_UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionString = c.String(),
                        User1_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Users", t => t.User1_UserId)
                .Index(t => t.User1_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "CurrentUser_UserId", "dbo.Users");
            DropForeignKey("dbo.Questions", "User1_UserId", "dbo.Users");
            DropForeignKey("dbo.Answers", "CurrentQuestion_QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "User1_UserId" });
            DropIndex("dbo.Answers", new[] { "CurrentUser_UserId" });
            DropIndex("dbo.Answers", new[] { "CurrentQuestion_QuestionId" });
            DropTable("dbo.Users");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
