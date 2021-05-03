namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypNameToNameInItemType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemTypes", "Name", c => c.String());
            DropColumn("dbo.ItemTypes", "TypName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemTypes", "TypName", c => c.String());
            DropColumn("dbo.ItemTypes", "Name");
        }
    }
}
