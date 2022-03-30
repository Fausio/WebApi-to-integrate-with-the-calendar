using Microsoft.AspNetCore.Mvc; 
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestGoogleCalendarAPI_MVC.Controllers
{
    public class CalindarEventController : Controller
    {
        public void CallBack(string code, string error, string state)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                this.GetTokens(code);
            }
        }


        public IActionResult createForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalindarEvent()
        {
            return View();
        }


        public IActionResult OauthRedirect()
        {
            var credentialsFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", "client_secret.json");
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var client_id = credentials["client_id"];

            var redirectUrl = $@"https://accounts.google.com/o/oauth2/v2/auth?
                                 scope=https://www.googleapis.com/auth/calendar+https://www.googleapis.com/auth/calendar.events&
                                 access_type=offline&
                                 include_granted_scopes=true&
                                 response_type=code&
                                 state=HelloTest&
                                 redirect_uri=https://localhost:44325/CalindarEvent/CallBack&
                                 client_id={client_id}";

            return Redirect(redirectUrl);
        }

        public   IActionResult  GetTokens(string code)
        {
            var TokenFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var credentialsFile = Path.Combine(Directory.GetCurrentDirectory(), "Files", "client_secret.json");
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("grant_type", "authorization_code");
            request.AddQueryParameter("redirect_uri", "https://localhost:44325/CalindarEvent/CallBack");

            restClient.BaseUrl = new Uri("https://oauth2.googleapis.com/token");
            var response =   restClient.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(TokenFile, response.Content);
                return RedirectToAction("Index", "Home");
            }

            return View("ErrorView");
        }
    }
}
