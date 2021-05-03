namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ItemTypeId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        BorrowingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.ItemTypes", t => t.ItemTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ItemTypeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        Name = c.String(),
                        Designation = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ItemTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.ItemTypes", t => t.ItemTypeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ItemTypeId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magazines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MagazineTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Publisher = c.String(),
                        Source = c.String(),
                        SerialNumber = c.Int(nullable: false),
                        DateEntered = c.DateTime(nullable: false),
                        CabinetNumber = c.Int(nullable: false),
                        ShelfNumber = c.Int(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MagazineTypes", t => t.MagazineTypeId, cascadeDelete: true)
                .Index(t => t.MagazineTypeId);
            
            CreateTable(
                "dbo.MagazineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfficialMagazines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Publisher = c.String(),
                        Source = c.String(),
                        DateEntered = c.DateTime(nullable: false),
                        CabinetNumber = c.Int(nullable: false),
                        ShelfNumber = c.Int(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Books", "SerialNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Magazines", "MagazineTypeId", "dbo.MagazineTypes");
            DropForeignKey("dbo.EmployeeLogs", "ItemTypeId", "dbo.ItemTypes");
            DropForeignKey("dbo.EmployeeLogs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Borrows", "ItemTypeId", "dbo.ItemTypes");
            DropForeignKey("dbo.Borrows", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Magazines", new[] { "MagazineTypeId" });
            DropIndex("dbo.EmployeeLogs", new[] { "ItemTypeId" });
            DropIndex("dbo.EmployeeLogs", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Borrows", new[] { "ItemTypeId" });
            DropIndex("dbo.Borrows", new[] { "EmployeeId" });
            AlterColumn("dbo.Books", "SerialNumber", c => c.String());
            DropTable("dbo.OfficialMagazines");
            DropTable("dbo.MagazineTypes");
            DropTable("dbo.Magazines");
            DropTable("dbo.Languages");
            DropTable("dbo.EmployeeLogs");
            DropTable("dbo.ItemTypes");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Borrows");
        }
    }
}
