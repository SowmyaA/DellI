using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Members;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Interfaces
{
    public interface IMembersService
    {
        Task<Member> GetMembers();

        Task<Member> GetPersonById(string personId);
    }
}
