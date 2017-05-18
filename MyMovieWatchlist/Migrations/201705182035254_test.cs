namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "SiteMenuId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "SiteMenuId", c => c.Int(nullable: false));
        }
    }
}
