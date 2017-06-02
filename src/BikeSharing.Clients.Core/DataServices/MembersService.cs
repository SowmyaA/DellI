using BikeSharing.Clients.Core.DataServices.Base;
using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BikeSharing.Clients.Core.DataServices
{
    public class MembersService : IMembersService
    {
        private readonly IRequestProvider _requestProvider;      

        public MembersService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<Member> GetMembers()
        {         
            UriBuilder builder = new UriBuilder(GlobalSettings.CalenderEndpoint);
            builder.Path = "api/calender/latest";
            string uri = builder.ToString();

            //try
            //{
            //    HttpClient _client = new HttpClient();
            //    var response = await _client.GetStringAsync(uri);

            //    if (!string.IsNullOrEmpty(response))
            //    {
            //        var data = JsonConvert.DeserializeObject(response);
            //        //foreach (var people in data)
            //        //{
            //        //    Peoples.Add(people);
            //        //}                  
            //    }
            //}
            //catch (Exception)
            //{
            //  //  DisplayAlert("Error", "Something went wrong", "Ok");
            //}

            Member members = await _requestProvider.GetAsync<Member>(uri);
            //  IEnumerable<Member> events = await _requestProvider.GetAsync<IEnumerable<Member>>(uri);

            return members;
        }

        public async Task<Member> GetPersonById(string eventId)
        {
            var allEvents = await GetMembers();

            //  return allEvents.FirstOrDefault(e => e.Id == eventId);
            return allEvents;
        }
    }
}
