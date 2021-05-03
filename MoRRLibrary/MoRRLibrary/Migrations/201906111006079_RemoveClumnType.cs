namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveClumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime());
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime(storeType: "date"));
        }
    }
}
