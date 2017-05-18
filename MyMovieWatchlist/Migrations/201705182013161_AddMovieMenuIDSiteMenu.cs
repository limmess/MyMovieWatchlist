namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieMenuIDSiteMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "SiteMenuId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "SiteMenu_MenuId", c => c.Int());
            CreateIndex("dbo.Movies", "SiteMenu_MenuId");
            AddForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus", "MenuId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenu_MenuId" });
            DropColumn("dbo.Movies", "SiteMenu_MenuId");
            DropColumn("dbo.Movies", "SiteMenuId");
        }
    }
}
