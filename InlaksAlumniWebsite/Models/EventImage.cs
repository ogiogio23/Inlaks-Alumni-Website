using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InlaksAlumniWebsite.Models
{
    public class EventImage
    {
        [Key]
        public int ImageId { get; set; }

        public string ImagesUrl { get; set; }

        public string Name { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}