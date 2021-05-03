namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSerialNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficialMagazines", "SerialNumber", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.OfficialMagazines", "SerialNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.OfficialMagazines", new[] { "SerialNumber" });
            DropColumn("dbo.OfficialMagazines", "SerialNumber");
        }
    }
}
