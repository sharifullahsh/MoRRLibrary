namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequredToSomeField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.String(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "Publisher", c => c.String(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "PublicationDate", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "Name", c => c.String());
            AlterColumn("dbo.OfficialMagazines", "Publisher", c => c.String());
            AlterColumn("dbo.Magazines", "PublicationDate", c => c.String());
            AlterColumn("dbo.Magazines", "Name", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
        }
    }
}
