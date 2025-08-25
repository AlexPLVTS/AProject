using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Activity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Title { get; set; }

        public DateTime Date {  get; set; }

        public required string Description { get; set; }

        public required string Category { get; set; }

        public bool IsCanceled { get; set; }

        public required string City { get; set; }

        public required string Venue { get; set; }
        
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<ActivityAttendee> Atendees { get; set; } = [];
    }
}
