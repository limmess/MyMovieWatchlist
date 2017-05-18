namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieSiteMenuId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "SiteMenu_Id", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenu_Id" });
            RenameColumn(table: "dbo.Movies", name: "SiteMenu_Id", newName: "SiteMenuId");
            AlterColumn("dbo.Movies", "SiteMenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "SiteMenuId");
            AddForeignKey("dbo.Movies", "SiteMenuId", "dbo.SiteMenus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "SiteMenuId", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenuId" });
            AlterColumn("dbo.Movies", "SiteMenuId", c => c.Int());
            RenameColumn(table: "dbo.Movies", name: "SiteMenuId", newName: "SiteMenu_Id");
            CreateIndex("dbo.Movies", "SiteMenu_Id");
            AddForeignKey("dbo.Movies", "SiteMenu_Id", "dbo.SiteMenus", "Id");
        }
    }
}
