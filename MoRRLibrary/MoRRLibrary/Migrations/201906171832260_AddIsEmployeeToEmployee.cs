namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsEmployeeToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IsEmployee", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "IsEmployee");
        }
    }
}
