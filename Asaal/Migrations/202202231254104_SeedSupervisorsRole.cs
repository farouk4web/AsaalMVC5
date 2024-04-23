namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedSupervisorsRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6b52f216-b89e-40ae-b49a-80d6c592422e', N'Supervisors')");
        }
        
        public override void Down()
        {
        }
    }
}
