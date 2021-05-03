namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToAllModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Languages", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Borrows", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployeeLogs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Magazines", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MagazineTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.OfficialMagazines", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfficialMagazines", "IsActive");
            DropColumn("dbo.MagazineTypes", "IsActive");
            DropColumn("dbo.Magazines", "IsActive");
            DropColumn("dbo.EmployeeLogs", "IsActive");
            DropColumn("dbo.ItemTypes", "IsActive");
            DropColumn("dbo.Departments", "IsActive");
            DropColumn("dbo.Employees", "IsActive");
            DropColumn("dbo.Borrows", "IsActive");
            DropColumn("dbo.Languages", "IsActive");
            DropColumn("dbo.Books", "IsActive");
        }
    }
}
