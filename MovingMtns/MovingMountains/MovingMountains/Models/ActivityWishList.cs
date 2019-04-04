using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovingMountains.Models
{
    public class ActivityWishList
    {
        [Key]
        public Guid activityId { get; set; }
        public int myInterestScore { get; set; }
        public string myInterestNotes { get; set; }
    }
}