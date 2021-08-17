namespace MoviesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBirthDateInCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET BirthDate = '1997-06-18' WHERE Id=1");
            Sql("UPDATE Customers SET BirthDate ='2012-12-28' WHERE Id=2");
            Sql("UPDATE Customers SET BirthDate = '2008-10-31' WHERE Id=3");
        }
        
        public override void Down()
        {
        }
    }
}
