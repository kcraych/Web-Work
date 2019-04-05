using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moving_Mountains.Models
{
    public class UserDemographic
    {
        [Key]
        public string userId { get; set; }
        public Guid locationId { get; set; }
    }
}