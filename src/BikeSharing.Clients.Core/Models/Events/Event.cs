using System;

namespace BikeSharing.Clients.Core.Models.Events
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Emoction { get; set; }

        public string Attendence { get; set; }

        public DateTime StartTime { get; set; }

        public string ExternalId { get; set; }

        public Venue Venue { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }

    }
}
