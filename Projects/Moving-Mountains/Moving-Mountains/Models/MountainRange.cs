using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Moving_Mountains.Enums.Enums;

namespace Moving_Mountains.Models
{
    public class MountainRange
    {
        [Key]
        public Guid rangeId { get; set; }
        public string rangeName { get; set; }
        public StateTerritory rangeTerritory { get; set; }
        public int rangeSqMiles { get; set; }
    }
}