namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsActiveFromBorrow : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Borrows", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Borrows", "IsActive", c => c.Boolean(nullable: false));
        }
    }
}
