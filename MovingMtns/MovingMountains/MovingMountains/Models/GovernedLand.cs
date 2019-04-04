using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovingMountains.Models
{
    public class GovernedLand
    {
        [Key]
        public Guid landId { get; set; }
        public bool isNationalPark { get; set; }
        public bool isStatePark { get; set; }
        public bool isNationalForest { get; set; }
        public string landName { get; set; }
    }
}