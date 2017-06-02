using BikeSharing.Clients.Core.DataServices.Interfaces;
using BikeSharing.Clients.Core.DataServices.Fake;
using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Events;
using BikeSharing.Clients.Core.Models.Members;
using BikeSharing.Clients.Core.Models.Rides;
using BikeSharing.Clients.Core.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Globalization;
using System.Linq;

namespace BikeSharing.Clients.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IEventsService _eventsService;
        private readonly IRidesService _ridesService;
        private readonly IMembersService _memberService;

        DateTime currentDate;

        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }

            set
            {
                currentDate = value;
                RaisePropertyChanged(() => CurrentDate);
            }
        }

        string newjoin = "0";

        public string Newjoin
        {
            get
            {
                return newjoin;
            }

            set
            {
                newjoin = value;
                RaisePropertyChanged(() => Newjoin);
            }
        }

        string notjoined = "0";

        public string NotJoined
        {
            get
            {
                return notjoined;
            }

            set
            {
                notjoined = value;
                RaisePropertyChanged(() => NotJoined);
            }
        }
        string joined = "0";

        public string Joined
        {
            get
            {
                return joined;
            }

            set
            {
                joined = value;
                RaisePropertyChanged(() => Joined);
            }
        }

        string invities = "12";

        public string Invities
        {
            get
            {
                return invities;
            }

            set
            {
                invities = value;
                RaisePropertyChanged(() => Invities);
            }
        }

        string location = "-";

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                RaisePropertyChanged(() => Location);
            }
        }

        string temp = "-";

        public string Temp
        {
            get
            {
                return temp;
            }

            set
            {
                temp = value;
                RaisePropertyChanged(() => Temp);
            }
        }

        public List<Trends> Data { get; set; }
        public ObservableCollection<Usage> UsageData { get; set; }

        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public ObservableCollection<Event> Events
        {
            get
            {
                return _events;
            }

            set
            {
                _events = value;
                RaisePropertyChanged(() => Events);
            }
        }

        private ObservableCollection<MeetingMembers> _members = new ObservableCollection<MeetingMembers>();

        public ObservableCollection<MeetingMembers> Members
        {
            get
            {
                return _members;
            }

            set
            {
                _members = value;
                RaisePropertyChanged(() => Members);
            }
        }

        private ObservableCollection<Suggestion> _suggestions = new ObservableCollection<Suggestion>();
        private ObservableCollection<Trends> _trends = new ObservableCollection<Trends>();
        private ObservableCollection<Usage> _usage = new ObservableCollection<Usage>();

        public ObservableCollection<Suggestion> Suggestions
        {
            get
            {
                return _suggestions;
            }

            set
            {
                _suggestions = value;
                RaisePropertyChanged(() => Suggestions);
            }
        }
        public ObservableCollection<Trends> Trends
        {
            get
            {
                return _trends;
            }

            set
            {
                _trends = value;
                RaisePropertyChanged(() => Trends);
            }
        }
        public ObservableCollection<Usage> Usage
        {
            get
            {
                return _usage;
            }

            set
            {
                _usage = value;
                RaisePropertyChanged(() => Usage);
            }
        }

        public ICommand ShowEventCommand => new Command<Event>(ShowEventAsync);

        public ICommand ShowCustomRideCommand => new Command(CustomRideAsync);

        public ICommand ShowRecommendedRideCommand => new Command<Suggestion>(RecommendedRideAsync);

        public HomeViewModel(IWeatherService weatherService, IEventsService eventsService, IRidesService ridesService, IMembersService memberService)
        {
            _weatherService = weatherService;
            _eventsService = eventsService;
            _ridesService = ridesService;
            _memberService = memberService;
            Data = new List<Trends>()
            {
              new Trends { Age1 = "21", Age2 = "25", EntryTime =  "10", AgeRange="21-25" },
                 new Trends { Age1 = "26", Age2 = "30", EntryTime =  "1", AgeRange="26-30" },
                  new Trends { Age1 = "31", Age2 = "35", EntryTime =  "2", AgeRange="31-35" },
                   new Trends { Age1 = "36", Age2 = "40", EntryTime =  "3", AgeRange="36-40" },
                    new Trends { Age1 = "41", Age2 = "50", EntryTime =  "5", AgeRange=">40" }
            };

            UsageData = new ObservableCollection<Usage>() {
                new Usage { UsageType="Happy", Percentage="75" },
                 new Usage { UsageType="Laughing", Percentage="10" },
                  new Usage { UsageType="Sad", Percentage="15" },
            };

        }

        public override async Task InitializeAsync(object navigationData)
        {
            CurrentDate = DateTime.Now;
            IsBusy = true;

            try
            {
                var members = _memberService.GetMembers();
                var weather = _weatherService.GetWeatherInfoAsync();
                var events = _eventsService.GetEvents();
                var suggestions = _ridesService.GetSuggestions();


                var tasks = new List<Task> { weather, events, suggestions, members };
                while (tasks.Count > 0)
                {
                    var finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);

                    if (finishedTask.Status == TaskStatus.RanToCompletion)
                    {
                        if (finishedTask == weather)
                        {
                            var weatherResults = await weather;
                            if (weatherResults is WeatherInfo)
                            {
                                var weatherInfo = weatherResults as WeatherInfo;
                                Location = weatherInfo.LocationName;
                                Temp = Math.Round(weatherInfo.Temp).ToString();
                            }
                        }
                        else if (finishedTask == events)
                        {
                            var eventsResults = await events;
                            Events = new ObservableCollection<Event>(eventsResults);
                        }
                        else if (finishedTask == suggestions)
                        {
                            var suggestionsResults = await suggestions;
                            Suggestions = new ObservableCollection<Suggestion>(suggestionsResults);
                        }
                        else if (finishedTask == members)
                        {
                            var memberResults = await members;
                            //Invities                       
                            Invitees[] invitiesObject = memberResults.Invitees;
                            Invities = invitiesObject.Length.ToString();
                            //Attendees
                            Attendees[] attendeesObject = memberResults.MeetingAttendees;
                            Joined = attendeesObject.Length.ToString();
                            //New or Introduers
                            HashSet<string> PersonIds = new HashSet<string>(invitiesObject.Select(s => s.PersonId));
                            var results = attendeesObject.Where(m => !PersonIds.Contains(m.PersonId));
                            Newjoin = results.Count().ToString();
                            //Not joined
                            int attendees_invited = Math.Abs(attendeesObject.Length - results.Count());
                            NotJoined = Math.Abs(invitiesObject.Length - attendees_invited).ToString();
                            //Members
                            Members = new ObservableCollection<MeetingMembers>();
                            foreach (Attendees person in attendeesObject)
                            {
                                MeetingMembers meetingmembers = new MeetingMembers();
                                if (person.EmpName.ToString().Trim()=="NaN")
                                    meetingmembers.EmpName = "NEW";
                                else
                                {
                                    meetingmembers.EmpName = person.EmpName;
                                }
                                meetingmembers.PersonId = person.PersonId;
                                if (person.Department.ToString().Trim() == "NaN")
                                    meetingmembers.Department = "NEW";
                                else { meetingmembers.Department = person.Department; }

                                meetingmembers.Gender = person.Gender;

                                if (person.Smile == "Sad")
                                    meetingmembers.Smiley = "Smiley1.png";
                                else if (person.Smile == "Smile")
                                { meetingmembers.Smiley = "Smiley.png"; }
                                else { meetingmembers.Smiley = "Smiley.png"; }

                                meetingmembers.ImagePath = GlobalSettings.MemberEndpoint.ToString() + person.BlobName;

                                meetingmembers.Attendence = "Off.png";

                                Members.Add(meetingmembers);
                            }

                            HashSet<string> Id_Invitees = new HashSet<string>(invitiesObject.Select(s => s.PersonId));
                            var notPresent = attendeesObject.Where(m => !Id_Invitees.Contains(m.PersonId));

                            foreach (var person in notPresent)
                            {
                                if (person.EmpName.ToString().Trim() != "NaN")
                                {
                                    MeetingMembers meetingmembers = new MeetingMembers();
                                    if (person.EmpName.ToString().Trim() == "NaN")
                                        meetingmembers.EmpName = "NEW";
                                    else
                                    {
                                        meetingmembers.EmpName = person.EmpName;
                                    }
                                    meetingmembers.PersonId = person.PersonId;
                                    if (person.Department.ToString().Trim() == "NaN")
                                        meetingmembers.Department = "NEW";
                                    else { meetingmembers.Department = person.Department; }

                                    meetingmembers.Gender = person.Gender;

                                    if (person.Smile == "Sad")
                                        meetingmembers.Smiley = "Smiley1.png";
                                    else if (person.Smile == "Smile")
                                    { meetingmembers.Smiley = "Smiley.png"; }
                                    else { meetingmembers.Smiley = "Smiley.png"; }

                                    meetingmembers.ImagePath = GlobalSettings.MemberEndpoint.ToString() + person.BlobName;

                                    meetingmembers.Attendence = "On.png";

                                    Members.Add(meetingmembers);
                                }
                            }


                            //  Members = new ObservableCollection<Member>(memberResults);
                        }
                    }
                }
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                await DialogService.ShowAlertAsync("Communication error", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data in: {ex}");
            }

            IsBusy = false;
        }

        private async void ShowEventAsync(Event @event)
        {
            if (@event != null)
            {
                await NavigationService.NavigateToAsync<EventSummaryViewModel>(@event);
            }
        }

        private async void CustomRideAsync()
        {
            await NavigationService.NavigateToAsync<CustomRideViewModel>();
        }

        private async void RecommendedRideAsync(object obj)
        {
            var suggestion = obj as Suggestion;

            if (suggestion != null)
            {
                var parameters = new CustomRideViewModel.NavigationParameter
                {
                    Latitude = suggestion.Latitude,
                    Longitude = suggestion.Longitude
                };

                await NavigationService.NavigateToAsync<CustomRideViewModel>(parameters);
            }
        }


    }
}
