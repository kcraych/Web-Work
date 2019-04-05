using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Moving_Mountains.Enums.Enums;

namespace Moving_Mountains.Models
{
    public class Location
    {
        public Guid locationId { get; set; }
        public LocationType locationType { get; set; }
        public string locationStreetAddress { get; set; }
        public string locationCity { get; set; }
        public State locationState { get; set; }
        public string locationZip { get; set; }
        public double locationLatitude { get; set; }
        public double locationLongitude { get; set; }
    }
}