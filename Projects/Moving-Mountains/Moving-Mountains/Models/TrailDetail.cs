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