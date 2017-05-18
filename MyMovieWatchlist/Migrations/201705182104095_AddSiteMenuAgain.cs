namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteMenuAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "SiteMenu_Id", c => c.Int());
            CreateIndex("dbo.Movies", "SiteMenu_Id");
            AddForeignKey("dbo.Movies", "SiteMenu_Id", "dbo.SiteMenus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "SiteMenu_Id", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenu_Id" });
            DropColumn("dbo.Movies", "SiteMenu_Id");
        }
    }
}
