namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSerialToCodeNumber : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OfficialMagazines", new[] { "SerialNumber" });
            AddColumn("dbo.OfficialMagazines", "CodeNumber", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.OfficialMagazines", "CodeNumber", unique: true);
            DropColumn("dbo.OfficialMagazines", "SerialNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfficialMagazines", "SerialNumber", c => c.String(nullable: false, maxLength: 100));
            DropIndex("dbo.OfficialMagazines", new[] { "CodeNumber" });
            DropColumn("dbo.OfficialMagazines", "CodeNumber");
            CreateIndex("dbo.OfficialMagazines", "SerialNumber", unique: true);
        }
    }
}
