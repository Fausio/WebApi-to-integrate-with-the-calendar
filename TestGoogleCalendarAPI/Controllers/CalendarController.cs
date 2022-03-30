using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestGoogleCalendarAPI.helpers;
using TestGoogleCalendarAPI.models;

namespace TestGoogleCalendarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateGoogleCalendar([FromBody] GoogleCalendar request)
        {
            return Ok(await GoogleCalendarHelper.CreateGoogleCalendar(request));
        }
    }
}
