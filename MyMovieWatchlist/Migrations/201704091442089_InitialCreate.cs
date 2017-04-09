namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        Title = c.String(),
                        Year = c.String(),
                        Rated = c.String(),
                        Released = c.String(),
                        Runtime = c.String(),
                        Genre = c.String(),
                        Director = c.String(),
                        Writer = c.String(),
                        Actors = c.String(),
                        Plot = c.String(),
                        Language = c.String(),
                        Country = c.String(),
                        Awards = c.String(),
                        Poster = c.String(),
                        Metascore = c.String(),
                        imdbRating = c.String(),
                        imdbVotes = c.String(),
                        imdbID = c.String(),
                        Type = c.String(),
                        DVD = c.String(),
                        BoxOffice = c.String(),
                        Production = c.String(),
                        Website = c.String(),
                        Response = c.String(),
                        SelectedForSave = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
