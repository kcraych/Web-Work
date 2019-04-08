namespace Moving_Mountains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFavoriteId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ActivityWishLists");
            AddColumn("dbo.ActivityWishLists", "favoriteId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ActivityWishLists", "favoriteId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ActivityWishLists");
            DropColumn("dbo.ActivityWishLists", "favoriteId");
            AddPrimaryKey("dbo.ActivityWishLists", "activityId");
        }
    }
}
