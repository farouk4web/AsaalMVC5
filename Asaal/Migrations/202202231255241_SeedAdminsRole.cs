namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdminsRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0181066c-9978-4655-8783-1c64f3f088fc', N'Admins')");
        }
        
        public override void Down()
        {
        }
    }
}
