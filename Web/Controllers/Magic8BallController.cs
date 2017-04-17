using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;

namespace Web_UI.Controllers
{
    public class Magic8BallController : Controller
    {
        private string _doSomethingBaseAddress;
        private string _doSomethingAPIUrl;
        public Magic8BallController()
        {
            _doSomethingBaseAddress = "http://api";
            _doSomethingAPIUrl = "/Magic8BallApi";
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            HttpResponseMessage response = null;
            //
            // Get the HttpRequest
            //
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, 
                        _doSomethingBaseAddress + _doSomethingAPIUrl);

                response = client.SendAsync(request).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // eat the exception for now...
            }

            //
            // Return a response from the Crazy 8 Ball Service
            //
            if (response != null && response.IsSuccessStatusCode)
            {
                List<Dictionary<String, String>> responseElements = new List<Dictionary<String, String>>();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                String responseString = response.Content.ReadAsStringAsync().Result;
                ViewData["Answer"] = responseString;

            }
            return View();
        }
    }
}
