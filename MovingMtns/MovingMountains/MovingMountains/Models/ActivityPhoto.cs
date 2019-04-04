using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovingMountains.Models
{
    public class ActivityPhoto
    {
        [Key]
        public Guid photoId { get; set; }
        public Guid completeId { get; set; }
        public string myActivityPhoto { get; set; }
    }
}