namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNotificationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        TargetUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TargetUserId)
                .Index(t => t.AnswerId)
                .Index(t => t.TargetUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "TargetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Notifications", new[] { "TargetUserId" });
            DropIndex("dbo.Notifications", new[] { "AnswerId" });
            DropTable("dbo.Notifications");
        }
    }
}
