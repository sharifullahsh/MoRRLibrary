namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguageToBookModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Books", "LanguageId");
            AddForeignKey("dbo.Books", "LanguageId", "dbo.Languages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Books", new[] { "LanguageId" });
        }
    }
}
