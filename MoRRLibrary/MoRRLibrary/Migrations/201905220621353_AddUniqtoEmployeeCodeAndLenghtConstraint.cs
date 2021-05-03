namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqtoEmployeeCodeAndLenghtConstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "EmployeeCode", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "Designation", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Employees", "Phone", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.Employees", "EmployeeCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "EmployeeCode" });
            AlterColumn("dbo.Employees", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Designation", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "EmployeeCode", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
        }
    }
}
