namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.String());
            AlterColumn("dbo.Borrows", "RetrunDate", c => c.String());
            AlterColumn("dbo.Magazines", "DateEntered", c => c.String());
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Magazines", "DateEntered", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Borrows", "RetrunDate", c => c.DateTime());
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime());
        }
    }
}
