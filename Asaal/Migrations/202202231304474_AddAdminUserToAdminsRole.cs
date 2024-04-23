namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminUserToAdminsRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'faa86e49-7b77-426b-9021-a1602d967184', N'0181066c-9978-4655-8783-1c64f3f088fc')");
        }
        
        public override void Down()
        {
        }
    }
}
