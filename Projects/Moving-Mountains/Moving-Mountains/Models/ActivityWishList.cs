using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Models
{
    public class ActivityWishList
    {
        public ActivityWishList()
        {
            favoriteId = Guid.NewGuid();
        }

        [Key]
        public Guid favoriteId { get; set; }
        public Guid activityId { get; set; }
        public int myInterestScore { get; set; }
        public string myInterestNotes { get; set; }
    }

}