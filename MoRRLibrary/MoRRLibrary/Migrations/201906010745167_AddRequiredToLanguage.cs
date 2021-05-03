namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToLanguage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Languages", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Languages", "Name", c => c.String());
        }
    }
}
