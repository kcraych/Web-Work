namespace Moving_Mountains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActivityLatLongToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityDetails", "activityLatitude", c => c.Double(nullable: false));
            AlterColumn("dbo.ActivityDetails", "activityLongitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityDetails", "activityLongitude", c => c.Single(nullable: false));
            AlterColumn("dbo.ActivityDetails", "activityLatitude", c => c.Single(nullable: false));
        }
    }
}
