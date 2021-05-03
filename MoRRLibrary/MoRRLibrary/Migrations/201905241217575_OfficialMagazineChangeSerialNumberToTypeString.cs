namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OfficialMagazineChangeSerialNumberToTypeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OfficialMagazines", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "SerialNumber", c => c.Int(nullable: false));
        }
    }
}
