namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCooupleMovieProperties : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "Response");
            DropColumn("dbo.Movies", "SelectedForSave");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "SelectedForSave", c => c.String());
            AddColumn("dbo.Movies", "Response", c => c.String());
        }
    }
}
