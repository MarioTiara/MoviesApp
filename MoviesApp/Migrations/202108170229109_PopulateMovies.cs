namespace MoviesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES (1, 'Hangover', 5, '2020-08-27', '2020-07-25', 50)");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES (2, 'Die Hard', 1, '2019-03-15', '2019-03-02', 120)");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES (3, 'The Terminator', 1, '2021-04-30', '2021-04-20', 150)");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES (4, 'Toy Story', 3, '2015-07-11', '2015-07-05', 20)");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, DateAdded, ReleaseDate, NumberInStock) VALUES (5, 'Titanic', 4, '1998-08-06', '2010-05-25', 20)");
        }
        
        public override void Down()
        {
        }
    }
}
