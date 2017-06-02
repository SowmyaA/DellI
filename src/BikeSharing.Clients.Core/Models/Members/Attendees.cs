using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Models.Members
{
    public class Attendees
    {
        public string PersonId { get; set; }
        public DateTime TimeEntered { get; set; }
        public string Smile { get; set; }
        public string Gender { get; set; }
        public string BlobName { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
    }
}
