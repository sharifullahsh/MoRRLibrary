namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvailableQuantityToBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AvailableQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AvailableQuantity");
        }
    }
}
