namespace MyMovieWatchlist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMovieModelParentIDNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "ParentId", c => c.Int(nullable: false));
        }
    }
}
