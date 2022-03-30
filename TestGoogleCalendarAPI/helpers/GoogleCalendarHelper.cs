using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestGoogleCalendarAPI.models;

namespace TestGoogleCalendarAPI.helpers
{
    public class GoogleCalendarHelper
    {
        protected GoogleCalendarHelper()
        {

        }


        public static async Task<Event> CreateGoogleCalendar(GoogleCalendar request)
        {
            string[] scopes = { "https://www.googleapis.com/auth/calendar" };
            string AppName = "TestGoogleCalendarAPI";
            UserCredential Credential;

            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "credencial", "GoogleCalindar", "client_secret.json"), FileMode.Open, FileAccess.Read))
            {
                string credencialPath = "token.json";
                Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(

                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credencialPath, true)).Result;
                 
            }

            // difine services 
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = AppName
            });

            // define request
            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.Location,
                Start = new EventDateTime
                {
                    DateTime = request.Start,
                    TimeZone = "Africa/Maputo"
                },

                End = new EventDateTime
                {
                    DateTime = request.End,
                    TimeZone = "Africa/Maputo"
                },

                Description = request.Description
            };

            var eventRequest = services.Events.Insert(eventCalendar, "primary");
            var requestCreate = await eventRequest.ExecuteAsync();

            return requestCreate;
        }
    }
}
