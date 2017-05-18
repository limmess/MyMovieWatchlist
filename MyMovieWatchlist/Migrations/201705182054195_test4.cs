namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus");
            DropIndex("dbo.Movies", new[] { "SiteMenu_MenuId" });
            DropColumn("dbo.Movies", "SiteMenu_MenuId");
            DropTable("dbo.SiteMenus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SiteMenus",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        NavUrl = c.String(),
                        ParentMenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
            AddColumn("dbo.Movies", "SiteMenu_MenuId", c => c.Int());
            CreateIndex("dbo.Movies", "SiteMenu_MenuId");
            AddForeignKey("dbo.Movies", "SiteMenu_MenuId", "dbo.SiteMenus", "MenuId");
        }
    }
}
