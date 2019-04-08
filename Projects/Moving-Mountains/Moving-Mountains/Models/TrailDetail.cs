using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Models
{
    public class TrailDetail
    {
        [Key]
        public Guid trailId { get; set; }

        //For the next 3 - what's more ideal...storing just the Id and using joins to access the object
        //information?  Or, storing the actual object?  I would assume the later can bring large
        //overhead issues depending on the size of the application.
        public Guid activityId { get; set; }
        public Guid rangeId { get; set; }
        public Guid landId { get; set; }
        public float trailMiles { get; set; }
        public int trailElevationMax { get; set; }
        public int trailElevationGain { get; set; }
        public string trailNotes { get; set; }
        public bool isHikeTrail { get; set; }
        public bool isBackpackTrail { get; set; }
        public bool isBikeTrail { get; set; }
        public bool isSkiTrail { get; set; }
        public bool isVisitorTrail { get; set; }
    }
}