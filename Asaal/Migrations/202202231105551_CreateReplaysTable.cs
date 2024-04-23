namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReplaysTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        PublishDate = c.DateTime(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.AnswerId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replays", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replays", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Replays", new[] { "UserId" });
            DropIndex("dbo.Replays", new[] { "AnswerId" });
            DropTable("dbo.Replays");
        }
    }
}
