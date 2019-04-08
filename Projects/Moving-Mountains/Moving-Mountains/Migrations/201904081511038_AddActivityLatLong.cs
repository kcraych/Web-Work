namespace Moving_Mountains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivityLatLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityDetails", "activityInTown", c => c.Boolean(nullable: false));
            AddColumn("dbo.ActivityDetails", "activityLatitude", c => c.Single(nullable: false));
            AddColumn("dbo.ActivityDetails", "activityLongitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityDetails", "activityLongitude");
            DropColumn("dbo.ActivityDetails", "activityLatitude");
            DropColumn("dbo.ActivityDetails", "activityInTown");
        }
    }
}
