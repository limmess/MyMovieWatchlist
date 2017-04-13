namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMovieModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Title");
            DropColumn("dbo.Movies", "Year");
            DropColumn("dbo.Movies", "Rated");
            DropColumn("dbo.Movies", "Released");
            DropColumn("dbo.Movies", "Runtime");
            DropColumn("dbo.Movies", "Genre");
            DropColumn("dbo.Movies", "Director");
            DropColumn("dbo.Movies", "Writer");
            DropColumn("dbo.Movies", "Actors");
            DropColumn("dbo.Movies", "Plot");
            DropColumn("dbo.Movies", "Language");
            DropColumn("dbo.Movies", "Country");
            DropColumn("dbo.Movies", "Awards");
            DropColumn("dbo.Movies", "Poster");
            DropColumn("dbo.Movies", "imdbRating");
            DropColumn("dbo.Movies", "imdbVotes");
            DropColumn("dbo.Movies", "Type");
            DropColumn("dbo.Movies", "DVD");
            DropColumn("dbo.Movies", "BoxOffice");
            DropColumn("dbo.Movies", "Production");
            DropColumn("dbo.Movies", "Website");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Website", c => c.String());
            AddColumn("dbo.Movies", "Production", c => c.String());
            AddColumn("dbo.Movies", "BoxOffice", c => c.String());
            AddColumn("dbo.Movies", "DVD", c => c.String());
            AddColumn("dbo.Movies", "Type", c => c.String());
            AddColumn("dbo.Movies", "imdbVotes", c => c.String());
            AddColumn("dbo.Movies", "imdbRating", c => c.String());
            AddColumn("dbo.Movies", "Poster", c => c.String());
            AddColumn("dbo.Movies", "Awards", c => c.String());
            AddColumn("dbo.Movies", "Country", c => c.String());
            AddColumn("dbo.Movies", "Language", c => c.String());
            AddColumn("dbo.Movies", "Plot", c => c.String());
            AddColumn("dbo.Movies", "Actors", c => c.String());
            AddColumn("dbo.Movies", "Writer", c => c.String());
            AddColumn("dbo.Movies", "Director", c => c.String());
            AddColumn("dbo.Movies", "Genre", c => c.String());
            AddColumn("dbo.Movies", "Runtime", c => c.String());
            AddColumn("dbo.Movies", "Released", c => c.String());
            AddColumn("dbo.Movies", "Rated", c => c.String());
            AddColumn("dbo.Movies", "Year", c => c.String());
            AddColumn("dbo.Movies", "Title", c => c.String());
        }
    }
}
