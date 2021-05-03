namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnotationToEmployee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EmployeeCode", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Designation", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Phone", c => c.String());
            AlterColumn("dbo.Employees", "Designation", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            AlterColumn("dbo.Employees", "EmployeeCode", c => c.String());
        }
    }
}
