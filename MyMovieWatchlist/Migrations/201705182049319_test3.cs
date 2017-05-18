namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ParentId", c => c.Int());
        }
    }
}
