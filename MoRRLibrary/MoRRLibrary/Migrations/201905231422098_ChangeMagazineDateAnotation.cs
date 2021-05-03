namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMagazineDateAnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Magazines", "DateEntered", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Magazines", "DateEntered", c => c.DateTime(nullable: false));
        }
    }
}
