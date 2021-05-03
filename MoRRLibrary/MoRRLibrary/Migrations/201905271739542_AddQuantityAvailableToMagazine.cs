namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantityAvailableToMagazine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Magazines", "AvailableQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Magazines", "AvailableQuantity");
        }
    }
}
