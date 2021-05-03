namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'12c6ebcd-beb6-4216-b7e5-7268ea6503db', NULL, 0, N'ANS3JIxVHvzi4/hTWA7RgEA7YyOGVD+iXHRjs5yiMYmJVobuBx0sADr1y+p3iMenvQ==', N'6b3f8eb0-950d-40dc-aaeb-aa68d34bbc92', NULL, 0, 0, NULL, 1, 0, N'admin')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'53ac3e84-7f32-451b-bd27-8fb768a9f673', N'Admin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'12c6ebcd-beb6-4216-b7e5-7268ea6503db', N'53ac3e84-7f32-451b-bd27-8fb768a9f673')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
