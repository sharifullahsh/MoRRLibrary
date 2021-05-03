namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsRurnedAndReturnDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Borrows", "IsReturned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Borrows", "RetrunDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Borrows", "RetrunDate");
            DropColumn("dbo.Borrows", "IsReturned");
        }
    }
}
