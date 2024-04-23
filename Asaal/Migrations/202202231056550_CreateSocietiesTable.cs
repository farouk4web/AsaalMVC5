namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSocietiesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Societies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Societies", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Societies", new[] { "UserId" });
            DropTable("dbo.Societies");
        }
    }
}
