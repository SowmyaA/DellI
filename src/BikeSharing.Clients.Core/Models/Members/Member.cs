using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Models.Members
{

    public class MeetingInvitee
    {
        public string PersonId { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }

    }

    public class MeetingMembers
    {
        public string PersonId { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }        
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public string Smiley { get; set; }

        public string Attendence { get; set; }
    }

        public class Member
    {
        private DateTime created = DateTime.Now;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get;
            set;
        }
        public string MeetingRoomId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Invitees[] Invitees { get; set; }

        public Attendees[] MeetingAttendees { get; set; }

        public double Confidence { get; set; }

        public DateTime Created
        {
            get { return created; }
            set
            {
                if (value == null) created = DateTime.Now; else created = value;
            }
        }

    }   
}
