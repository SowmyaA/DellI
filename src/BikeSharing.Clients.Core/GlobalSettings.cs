namespace BikeSharing.Clients.Core
{
    public static class GlobalSettings
    {
        public const string AuthenticationEndpoint = "http://bikesharing-services-profileseeqwm7qksmuce.azurewebsites.net/";
        public const string EventsEndpoint = "http://bikesharing-services-eventseeqwm7qksmuce.azurewebsites.net/";
        public const string IssuesEndpoint = "http://bikesharing-services-feedbackeeqwm7qksmuce.azurewebsites.net/";
        public const string RidesEndpoint = "http://bikesharing-services-rideseeqwm7qksmuce.azurewebsites.net/";

        public const string CalenderEndpoint = "http://smrtmeetdev-backendi7v3j2pn2fltq.azurewebsites.net/";
        public const string MemberEndpoint = "http://smrtmeetdev-backendi7v3j2pn2fltq.azurewebsites.net/api/image/";

        public const string OpenWeatherMapAPIKey = "4649727ddf0effc6cca3a04bd7a1e2a2";

        public const string HockeyAppAPIKeyForAndroid = "YOUR_HOCKEY_APP_ID";
        public const string HockeyAppAPIKeyForiOS = "YOUR_HOCKEY_APP_ID";

        public const string SkypeBotAccount = "skype:sowmya?chat";

        public const string BingMapsAPIKey = "AkQBScf1bOU3IT0pbaRH20rf4xg0NmW-i8OQM_FOLlbFROBGEfUnnmLvk8Y4xVTI";

        public static string City => "Bangalore";

        public static int TenantId = 1;
    }
}
