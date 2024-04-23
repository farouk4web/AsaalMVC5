namespace Asaal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FullName], [ProfileImgSrc], [CountryCode])
                    VALUES (N'faa86e49-7b77-426b-9021-a1602d967184', N'farouk@asaal.com', 0, N'AAhDb/C+nK6fNHpFtlJjA6QJmE8CJarantzHP0dfsN778RDSIHDlxQAatyBZdhmKBg==', N'4b75dac8-6314-4bdc-ae24-f76cfc32dbaf', NULL, 0, 0, NULL, 1, 0, N'farouk@asaal.com', N'Farouk Abdelhamid', N'/Content/user.jpg', N'EG')");
        }
        
        public override void Down()
        {
        }
    }
}
