using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStoreWebClient.Models;
using System.Net.Http;

namespace ODataBookStoreWebClient.Controllers
{
    public class PressesController : Controller
    {
        HttpClient _httpClient;
        public PressesController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5271/odata/")
            };

            // Set the desired request headers (if needed)
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Presses");
            string data = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(data);
            var list = temp.value;
            List<Press> items = ((JArray)temp.value).Select(x => new Press
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"]
            }).ToList();
            return View(items);
        }
    }
}
