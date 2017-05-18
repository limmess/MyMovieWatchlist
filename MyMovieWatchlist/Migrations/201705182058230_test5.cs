namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        NavUrl = c.String(),
                        ParentMenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SiteMenus");
        }
    }
}
