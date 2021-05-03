namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEmployeeLogModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeLogs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EmployeeLogs", "ItemTypeId", "dbo.ItemTypes");
            DropIndex("dbo.EmployeeLogs", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeLogs", new[] { "ItemTypeId" });
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Borrows", "RetrunDate", c => c.DateTime());
            DropTable("dbo.EmployeeLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmployeeLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ItemTypeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Borrows", "RetrunDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime(nullable: false, storeType: "date"));
            CreateIndex("dbo.EmployeeLogs", "ItemTypeId");
            CreateIndex("dbo.EmployeeLogs", "EmployeeId");
            AddForeignKey("dbo.EmployeeLogs", "ItemTypeId", "dbo.ItemTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EmployeeLogs", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
