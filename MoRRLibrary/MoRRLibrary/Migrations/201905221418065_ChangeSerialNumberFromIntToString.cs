namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSerialNumberFromIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "SerialNumber", c => c.Int(nullable: false));
        }
    }
}
