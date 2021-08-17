namespace MoviesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSubcirbedToCusotomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsSubscirbedToNewsLetter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsSubscirbedToNewsLetter");
        }
    }
}
