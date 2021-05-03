namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnotationToBorrowingDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Borrows", "BorrowingDate", c => c.DateTime(nullable: false));
        }
    }
}
