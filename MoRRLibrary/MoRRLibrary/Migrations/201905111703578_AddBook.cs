namespace MoRRLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new 
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Translator = c.String(),
                        Publisher = c.String(),
                        LanguageId = c.Int(nullable: false),
                        CabinetNumber = c.Int(nullable: false),
                        ShelfNumber = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
