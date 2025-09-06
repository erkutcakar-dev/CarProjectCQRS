using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "YOUR_API_KEY_HERE";

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string city = "Istanbul")
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&aqi=no";
            var response = await client.GetStringAsync(url);
            var json = JObject.Parse(response);

            // Kart tasarımı için liste haline getirelim (tek eleman da olsa liste)
            var weatherList = new List<dynamic>
            {
                new {
                    City = (string)json["location"]["name"],
                    Date = (string)json["location"]["localtime"],
                    TemperatureC = (double)json["current"]["temp_c"],
                    Condition = (string)json["current"]["condition"]["text"],
                    WindKph = (double)json["current"]["wind_kph"],
                    Humidity = (int)json["current"]["humidity"]
                }
            };

            return View(weatherList);
        }
    }
}
