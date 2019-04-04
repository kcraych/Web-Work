using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovingMountains.Models
{
    public class ActivityComplete
    {
        [Key]
        public Guid completeId { get; set; }
        public Guid activityId { get; set; }
        public DateTime dateCompleted { get; set; }
        public float hrsToComplete { get; set; }
        public int myDifficultyScore { get; set; }
        public int myActivityScore { get; set; }
        public bool isFavorite { get; set; }
        public string myActivityNotes { get; set; }
    }
}