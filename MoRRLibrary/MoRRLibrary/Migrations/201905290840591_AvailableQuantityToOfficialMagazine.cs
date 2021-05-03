namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvailableQuantityToOfficialMagazine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficialMagazines", "AvailableQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfficialMagazines", "AvailableQuantity");
        }
    }
}
