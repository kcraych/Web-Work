namespace Moving_Mountains.Migrations
{
    using Moving_Mountains.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static Moving_Mountains.Enums.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<Moving_Mountains.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Moving_Mountains.Models.ApplicationDbContext context)
        {
            var locations = new List<Location>
            {
                new Location() { locationType=LocationType.Activity, locationStreetAddress="28215 CO-74", locationCity="Evergreen", locationState=State.CO, locationZip="80439", locationLatitude=39.633113, locationLongitude=-105.320305 },
                new Location() { locationType=LocationType.Activity, locationStreetAddress="722 Grand Ave", locationCity="Glenwood Springs", locationState=State.CO, locationZip="81601", locationLatitude=39.551206, locationLongitude=-107.323933 },
                new Location() { locationType=LocationType.Activity, locationStreetAddress="1517 Miner St", locationCity="Idaho Springs", locationState=State.CO, locationZip="80452", locationLatitude=39.752949, locationLongitude=-105.515832 },
                new Location() { locationType=LocationType.Activity, locationStreetAddress="70 E 1st St", locationCity="Nederland", locationState=State.CO, locationZip="80466", locationLatitude=39.962047, locationLongitude=-105.50935 }
            };
            context.Locations.AddOrUpdate(l => new { l.locationCity, l.locationState }, locations.ToArray());
            context.SaveChanges();

            var lands = new List<GovernedLand>
            {
                new GovernedLand() { landName="Genesee Park", isNationalPark=false, isStatePark=false, isNationalForest=false },
                new GovernedLand() { landName="White River National Forest", isNationalPark=false, isStatePark=false, isNationalForest=true },
                new GovernedLand() { landName="Arapaho National Forest", isNationalPark=false, isStatePark=false, isNationalForest=true }
            };
            context.GovernedLands.AddOrUpdate(l => l.landName, lands.ToArray());
            context.SaveChanges();
            
            var ranges = new List<MountainRange>
            {
                new MountainRange() { rangeName="Front Range", rangeTerritory=StateTerritory.North_Central, rangeSqMiles=9962 }
            };
            context.MountainRanges.AddOrUpdate(m => m.rangeName, ranges.ToArray());
            context.SaveChanges();
            
            var activities = new List<ActivityDetail>
            {
                new ActivityDetail() { locationId=locations.Where(x => x.locationCity + ", " + x.locationState == "Nederland, CO").FirstOrDefault().locationId, activityInTown=false, activityLatitude=40.033387, activityLongitude=-105.645056, activityType=ActivityType.Trail, activityName="Diamond Lake Trail", activityWinter=false, activitySpring=true, activitySummer=true, activityFall=true, activitySiteName="AllTrails", activitySiteURL="https://www.alltrails.com/trail/us/colorado/diamond-lake-trail" },
                new ActivityDetail() { locationId=locations.Where(x => x.locationCity + ", " + x.locationState == "Glenwood Springs, CO").FirstOrDefault().locationId, activityInTown=false, activityLatitude=39.58916, activityLongitude=-107.19043, activityType=ActivityType.Trail, activityName="Hanging Lake Trail", activityWinter=false, activitySpring=true, activitySummer=true, activityFall=true, activitySiteName="AllTrails", activitySiteURL="https://www.alltrails.com/trail/us/colorado/hanging-lake" },
                new ActivityDetail() { locationId=locations.Where(x => x.locationCity + ", " + x.locationState == "Evergreen, CO").FirstOrDefault().locationId, activityInTown=false, activityLatitude=39.58266, activityLongitude=-105.3608, activityType=ActivityType.Trail, activityName="Maxwell Falls Trail", activityWinter=true, activitySpring=true, activitySummer=true, activityFall=true, activitySiteName="AllTrails", activitySiteURL="https://www.alltrails.com/trail/us/colorado/maxwell-falls-lower-trail" },
                new ActivityDetail() { locationId=locations.Where(x => x.locationCity + ", " + x.locationState == "Evergreen, CO").FirstOrDefault().locationId, activityInTown=false, activityLatitude=39.71565, activityLongitude=-105.30931, activityType=ActivityType.Trail, activityName="Beaver Brook and Chavez Trail Loop", activityWinter=true, activitySpring=true, activitySummer=true, activityFall=true, activitySiteName="AllTrails", activitySiteURL="https://www.alltrails.com/trail/us/colorado/beaver-brook-chavez-trail-loop" },
                new ActivityDetail() { locationId=locations.Where(x => x.locationCity + ", " + x.locationState == "Idaho Springs, CO").FirstOrDefault().locationId, activityInTown=false, activityLatitude=39.82695, activityLongitude=-105.6434, activityType=ActivityType.Trail, activityName="Saint Mary's Glacier", activityNotes="Pay $5 cash to park.", activityWinter=true, activitySpring=true, activitySummer=true, activityFall=true, activitySiteName="AllTrails", activitySiteURL="https://www.alltrails.com/trail/us/colorado/st-mary-s-glacier" },
            };
            context.ActivityDetails.AddOrUpdate(a => new { a.activityName, a.locationId}, activities.ToArray());
            context.SaveChanges();

            var trails = new List<TrailDetail>
            {
                new TrailDetail() { activityId=activities.Where(x => x.activityName == "Diamond Lake Trail").FirstOrDefault().activityId, rangeId=ranges.Where(x => x.rangeName == "Front Range").FirstOrDefault().rangeId, landId=lands.Where(x => x.landName == "Genesee Park").FirstOrDefault().landId, trailMiles=5.7f, trailElevationMax=1240, trailElevationGain=10962, trailNotes="Best in spring for meadow flowers", isHikeTrail=true, isBackpackTrail=true, isBikeTrail=false, isSkiTrail=false, isVisitorTrail=true },
                new TrailDetail() { activityId=activities.Where(x => x.activityName == "Hanging Lake Trail").FirstOrDefault().activityId, landId=lands.Where(x => x.landName == "White River National Forest").FirstOrDefault().landId, trailMiles=2.8f, trailElevationMax=1207, trailElevationGain=7292, trailNotes="Pretty, but crowded", isHikeTrail=true, isBackpackTrail=false, isBikeTrail=false, isSkiTrail=false, isVisitorTrail=true },
                new TrailDetail() { activityId=activities.Where(x => x.activityName == "Maxwell Falls Trail").FirstOrDefault().activityId, rangeId=ranges.Where(x => x.rangeName == "Front Range").FirstOrDefault().rangeId, landId=lands.Where(x => x.landName == "Arapaho National Forest").FirstOrDefault().landId, trailMiles=4.2f, trailElevationMax=875, trailElevationGain=8412, isHikeTrail=true, isBackpackTrail=false, isBikeTrail=false, isSkiTrail=false, isVisitorTrail=true },
                new TrailDetail() { activityId=activities.Where(x => x.activityName == "Beaver Brook and Chavez Trail Loop").FirstOrDefault().activityId, rangeId=ranges.Where(x => x.rangeName == "Front Range").FirstOrDefault().rangeId, landId=lands.Where(x => x.landName == "Genesee Park").FirstOrDefault().landId, trailMiles=5.1f, trailElevationMax=1105, trailElevationGain=7583, trailNotes="Boring, but nice walk if mountains are snowy", isHikeTrail=true, isBackpackTrail=false, isBikeTrail=false, isSkiTrail=false, isVisitorTrail=true },
                new TrailDetail() { activityId=activities.Where(x => x.activityName == "Saint Mary's Glacier").FirstOrDefault().activityId, rangeId=ranges.Where(x => x.rangeName == "Front Range").FirstOrDefault().rangeId, landId=lands.Where(x => x.landName == "Arapaho National Forest").FirstOrDefault().landId, trailMiles=1.9f, trailElevationMax=807, trailElevationGain=11202, trailNotes="Pretty and short, but crowded", isHikeTrail=true, isBackpackTrail=false, isBikeTrail=false, isSkiTrail=true, isVisitorTrail=true }
            };
            context.TrailDetails.AddOrUpdate(t => t.activityId, trails.ToArray());
            context.SaveChanges();
        }
    }
}
