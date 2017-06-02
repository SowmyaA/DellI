using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models.Events;
using System.Linq;

namespace BikeSharing.Clients.Core.DataServices.Fake
{
    public class FakeEventsService : IEventsService
    {
        private static List<Event> events = new List<Event>
        {
            new Event
            {
               // Name = "NBA Match",
               Name = "Sowmya A",
                Venue = new Venue
                {
                    Name = "New York Knicks vs. Brooklyn Nets"
                },
                StartTime = DateTime.Now,
                ImagePath = @"sowmya.png",
                Attendence = @"Off.png",
                Emoction=@"Smiley.png",
                Email = "sowmya.a2@dell.com",
                PhoneNumber = "91 804 0027918",
                Title = "Consultant"
               // ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_nba-match.png"
            },
            new Event
            {
               // Name = "Music Ride",
                Name="Rajesh Pillai",
                Venue = new Venue
                {
                    Name = "Green day"
                },
                StartTime = DateTime.Now,
                 //ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_music-ride.png"
                ImagePath = @"Rajesh.png",
                Attendence = @"Off.png",
                Emoction=@"Smiley.png",
                Email = "rajesh.pillai@dell.com",
                PhoneNumber = "91 804 0027838",
                Title = "Solutions Architect"
            },
            new Event
            {
               // Name = "Music Ride",
                Name="Kumar Krishnamurthy",
                Venue = new Venue
                {
                    Name = "Green day"
                },
                StartTime = DateTime.Now,
                // ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_music-ride.png"
                ImagePath = @"Kumar.png",
                 Attendence = @"Off.png",
                Emoction=@"Smiley1.png",
                Email = "kumar.krishnamurthy@dell.com",
                PhoneNumber = "91 894 0027918",
                Title = "Consultant II"
            },
            new Event
            {
               // Name = "Music Ride",
                Name="Kamath Narasinha",
                Venue = new Venue
                {
                    Name = "Green day"
                },
                StartTime = DateTime.Now,
               // ImagePath = "https://connect16test.blob.core.windows.net/imgs/i_music-ride.png"
                ImagePath = @"Praveen.png",
                 Attendence = @"On.png",
                Emoction=@"Smiley1.png",
                Email = "kamath.narasinha@dell.com",
                PhoneNumber = "91 822 0027668",
                Title = "Consultant II"
            } 
            
            
        };

        public Task<Event> GetEventById(int eventId)
        {
            var data = events[0];

            return Task.FromResult(data);
        }

        public Task<IEnumerable<Event>> GetEvents()
        {
            var data = Enumerable.Range(0, 4).Select((index) => events[index % events.Count]);

            return Task.FromResult(data);
        }
    }
}
