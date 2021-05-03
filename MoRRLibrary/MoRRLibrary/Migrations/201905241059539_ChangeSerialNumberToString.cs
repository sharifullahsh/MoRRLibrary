namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSerialNumberToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Magazines", "SerialNumber", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "ShelfNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "ShelfNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Magazines", "SerialNumber", c => c.Int(nullable: false));
        }
    }
}
