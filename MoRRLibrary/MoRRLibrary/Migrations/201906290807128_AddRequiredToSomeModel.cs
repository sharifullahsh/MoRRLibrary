namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToSomeModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Magazines", "DateEntered", c => c.String(nullable: false));
            AlterColumn("dbo.MagazineTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OfficialMagazines", "DateEntered", c => c.String());
            AlterColumn("dbo.MagazineTypes", "Name", c => c.String());
            AlterColumn("dbo.Magazines", "DateEntered", c => c.String());
        }
    }
}
