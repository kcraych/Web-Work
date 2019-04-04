namespace MovingMountains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityCompletes",
                c => new
                    {
                        completeId = c.Guid(nullable: false),
                        activityId = c.Guid(nullable: false),
                        dateCompleted = c.DateTime(nullable: false),
                        hrsToComplete = c.Single(nullable: false),
                        myDifficultyScore = c.Int(nullable: false),
                        myActivityScore = c.Int(nullable: false),
                        isFavorite = c.Boolean(nullable: false),
                        myActivityNotes = c.String(),
                    })
                .PrimaryKey(t => t.completeId);
            
            CreateTable(
                "dbo.ActivityPhotoes",
                c => new
                    {
                        photoId = c.Guid(nullable: false),
                        completeId = c.Guid(nullable: false),
                        myActivityPhoto = c.String(),
                    })
                .PrimaryKey(t => t.photoId);
            
            CreateTable(
                "dbo.ActivityWishLists",
                c => new
                    {
                        activityId = c.Guid(nullable: false),
                        myInterestScore = c.Int(nullable: false),
                        myInterestNotes = c.String(),
                    })
                .PrimaryKey(t => t.activityId);
            
            CreateTable(
                "dbo.GovernedLands",
                c => new
                    {
                        landId = c.Guid(nullable: false),
                        isNationalPark = c.Boolean(nullable: false),
                        isStatePark = c.Boolean(nullable: false),
                        isNationalForest = c.Boolean(nullable: false),
                        landName = c.String(),
                    })
                .PrimaryKey(t => t.landId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        locationId = c.Guid(nullable: false),
                        locationType = c.Int(nullable: false),
                        locationStreetAddress = c.String(),
                        locationCity = c.String(),
                        locationState = c.Int(nullable: false),
                        locationZip = c.String(),
                        locationLatitude = c.Double(nullable: false),
                        locationLongitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.locationId);
            
            CreateTable(
                "dbo.MountainRanges",
                c => new
                    {
                        rangeId = c.Guid(nullable: false),
                        rangeName = c.String(),
                        rangeTerritory = c.Int(nullable: false),
                        rangeSqMiles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.rangeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TrailDetails",
                c => new
                    {
                        trailId = c.Guid(nullable: false),
                        activityId = c.Guid(nullable: false),
                        rangeId = c.Guid(nullable: false),
                        landId = c.Guid(nullable: false),
                        trailMiles = c.Single(nullable: false),
                        trailElevationMax = c.Int(nullable: false),
                        trailElevationGain = c.Int(nullable: false),
                        trailNotes = c.String(),
                        isHikeTrail = c.Boolean(nullable: false),
                        isBackpackTrail = c.Boolean(nullable: false),
                        isBikeTrail = c.Boolean(nullable: false),
                        isSkiTrail = c.Boolean(nullable: false),
                        isVisitorTrail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.trailId);
            
            CreateTable(
                "dbo.UserDemographics",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128),
                        locationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserDemographics");
            DropTable("dbo.TrailDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MountainRanges");
            DropTable("dbo.Locations");
            DropTable("dbo.GovernedLands");
            DropTable("dbo.ActivityWishLists");
            DropTable("dbo.ActivityPhotoes");
            DropTable("dbo.ActivityCompletes");
        }
    }
}
