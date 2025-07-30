using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class DbInitializer
    {
        public static async Task SeedData(AppDbContext context)
        {
            if (context.Activities.Any()) return;

            var activities = new List<Activity>
            {
                new Activity
                {
                    Title = "Birthday Party",
                    Date = DateTime.Now.AddDays(7),
                    Description = "Celebrating John's 30th birthday.",
                    Category = "Party",
                    City = "New York",
                    Venue = "Downtown Club",
                    Latitude = 40.7128,
                    Longitude = -74.0060
                },
                new Activity
                {
                    Title = "Business Dinner",
                    Date = DateTime.Now.AddDays(-3),
                    Description = "Past Dinner with clients at the rooftop restaurant.",
                    Category = "Dinner",
                    City = "Chicago",
                    Venue = "Skyline Rooftop",
                    Latitude = 41.8781,
                    Longitude = -87.6298
                },
                new Activity
                {
                    Title = "Music Concert",
                    Date = DateTime.Now.AddDays(10),
                    Description = "Live concert at the downtown arena.",
                    Category = "Party",
                    City = "Los Angeles",
                    Venue = "LA Arena",
                    Latitude = 34.0522,
                    Longitude = -118.2437
                },
                new Activity
                {
                    Title = "Family Gathering",
                    Date = DateTime.Now.AddDays(5),
                    Description = "Annual family reunion event.",
                    Category = "Gathering",
                    City = "Houston",
                    Venue = "Family Park",
                    Latitude = 29.7604,
                    Longitude = -95.3698
                },
                new Activity
                {
                    Title = "Conference",
                    Date = DateTime.Now.AddDays(15),
                    Description = "Tech conference in downtown conference center.",
                    Category = "Conference",
                    City = "San Francisco",
                    Venue = "SF Convention Center",
                    Latitude = 37.7749,
                    Longitude = -122.4194
                },
                new Activity
                {
                    Title = "Charity Gala",
                    Date = DateTime.Now.AddDays(20),
                    Description = "Annual fundraising event.",
                    Category = "Gala",
                    City = "Miami",
                    Venue = "Luxury Hotel Ballroom",
                    Latitude = 25.7617,
                    Longitude = -80.1918
                },
                new Activity
                {
                    Title = "Team Lunch",
                    Date = DateTime.Now.AddDays(2),
                    Description = "Casual team lunch at the local restaurant.",
                    Category = "Meetup",
                    City = "Seattle",
                    Venue = "Seaside Bistro",
                    Latitude = 47.6062,
                    Longitude = -122.3321
                },
                new Activity
                {
                    Title = "Wedding Ceremony",
                    Date = DateTime.Now.AddDays(30),
                    Description = "John and Lisa's wedding celebration.",
                    Category = "Wedding",
                    City = "Austin",
                    Venue = "Sunset Gardens",
                    Latitude = 30.2672,
                    Longitude = -97.7431
                }
            };

            context.Activities.AddRange(activities);

            await context.SaveChangesAsync();
        }
    }
}
