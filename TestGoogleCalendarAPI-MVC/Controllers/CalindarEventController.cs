using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGoogleCalendarAPI_MVC.Controllers
{
    public class CalindarEventController : Controller
    {
        public IActionResult createForm()
        {
            return View();
        }
    }
}
