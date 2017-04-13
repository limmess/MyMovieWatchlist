namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMovieModelRemoveMetascore : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Metascore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Metascore", c => c.String());
        }
    }
}
