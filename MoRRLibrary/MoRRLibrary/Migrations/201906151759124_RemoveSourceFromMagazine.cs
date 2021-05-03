namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSourceFromMagazine : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Magazines", "Source");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Magazines", "Source", c => c.String());
        }
    }
}
