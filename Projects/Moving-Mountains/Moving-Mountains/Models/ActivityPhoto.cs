using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Models
{
    public class ActivityPhoto
    {

        public ActivityPhoto()
        {
            photoId = Guid.NewGuid();
        }

        [Key]
        public Guid photoId { get; set; }
        public Guid completeId { get; set; }
        public string myActivityPhoto { get; set; }
    }

}