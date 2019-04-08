using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Models
{
    public class GovernedLand
    {
        public GovernedLand()
        {
            landId = Guid.NewGuid();
        }

        [Key]
        public Guid landId { get; set; }
        public bool isNationalPark { get; set; }
        public bool isStatePark { get; set; }
        public bool isNationalForest { get; set; }
        public string landName { get; set; }
    }

}