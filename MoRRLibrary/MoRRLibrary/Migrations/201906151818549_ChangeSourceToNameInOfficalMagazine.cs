namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSourceToNameInOfficalMagazine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficialMagazines", "Name", c => c.String());
            DropColumn("dbo.OfficialMagazines", "Source");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OfficialMagazines", "Source", c => c.String());
            DropColumn("dbo.OfficialMagazines", "Name");
        }
    }
}
