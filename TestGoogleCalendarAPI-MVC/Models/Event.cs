using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGoogleCalendarAPI_MVC.Models
{
    public class Event
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }


        public Event()
        {
            Start = new EventDateTime()
            {
                TimeZone = "Maputo/Mozambique"
            };
            End = new EventDateTime()
            {
                TimeZone = "Maputo/Mozambique"
            };
        }
    }

    public class EventDateTime
    {
        public string DateTime { get; set; }
        public string TimeZone { get; set; }
    }
}
