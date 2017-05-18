namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenu_MenuId" });
            DropColumn("dbo.Movies", "SiteMenu_MenuId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "SiteMenu_MenuId", c => c.Int());
            CreateIndex("dbo.Movies", "SiteMenu_MenuId");
            AddForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus", "MenuId");
        }
    }
}
