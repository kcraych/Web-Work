using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Moving_Mountains.Enums.Enums;

namespace Moving_Mountains.Models
{
    public class ActivityDetail
    {
        [Key]
        public Guid activityId { get; set; }
        public Guid locationId { get; set; } //nearest town
        public bool activityInTown { get; set; }
        public float activityLatitude { get; set; }
        public float activityLongitude { get; set; }
        public float driveHrsFromHome { get; set; }
        public ActivityType activityType { get; set; }
        public string activityOtherDesc { get; set; }
        public string activityName { get; set; }
        public string activityNotes { get; set; }
        public bool activityWinter { get; set; }
        public bool activitySpring { get; set; }
        public bool activitySummer { get; set; }
        public bool activityFall { get; set; }
        public string activitySiteName { get; set; }
        public string activitySiteURL { get; set; }
    }

}