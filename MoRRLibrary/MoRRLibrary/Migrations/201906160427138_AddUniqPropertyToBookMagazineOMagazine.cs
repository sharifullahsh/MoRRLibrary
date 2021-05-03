namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqPropertyToBookMagazineOMagazine : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "SerialNumber", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Magazines", "SerialNumber", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.OfficialMagazines", "SerialNumber", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Books", "SerialNumber", unique: true);
            CreateIndex("dbo.Magazines", "SerialNumber", unique: true);
            CreateIndex("dbo.OfficialMagazines", "SerialNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.OfficialMagazines", new[] { "SerialNumber" });
            DropIndex("dbo.Magazines", new[] { "SerialNumber" });
            DropIndex("dbo.Books", new[] { "SerialNumber" });
            AlterColumn("dbo.OfficialMagazines", "SerialNumber", c => c.String());
            AlterColumn("dbo.Magazines", "SerialNumber", c => c.String());
            AlterColumn("dbo.Books", "SerialNumber", c => c.String());
        }
    }
}
