namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOfficalMagazineSerialNumberTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.OfficialMagazines", "ShelfNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "ShelfNumber", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.DateTime(nullable: false));
        }
    }
}
