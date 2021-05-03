namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForignKeyFromEmployeeAndDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
