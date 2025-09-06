using CarProjectCQRS.Models;
using CarProjectCQRS.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarProjectCQRS.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly CarProjectDbContext _context;
        private readonly string _eiaApiKey = "pYg323LIQgLqTXBozpGHkGJSgbhkw3XOFkmdAp2N";
        private readonly string _weatherApiKey = "d4474084274c49e5a69205838250609";
        private readonly string _exchangeApiKey = "0d19f0ca62150e41549a0688"; 

        public AdminController(HttpClient httpClient, CarProjectDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return await Dashboard();
        }

        public async Task<IActionResult> Dashboard()
        {
            // -------- Fuel Price --------
            List<EiaDataPoint> fuelData;
            try
            {
                string fuelUrl = $"https://api.eia.gov/v2/petroleum/pri/gnd/data/?api_key={_eiaApiKey}&frequency=weekly&data[0]=value&facets[product][]=EPMRU&sort[0][column]=period&sort[0][direction]=desc&offset=0&length=1";
                var result = await _httpClient.GetFromJsonAsync<EiaGasPriceResponse>(fuelUrl);
                fuelData = result?.response?.data ?? new List<EiaDataPoint>();
            }
            catch
            {
                fuelData = new List<EiaDataPoint>
                {
                    new EiaDataPoint { period = "API çalışmıyor", value = 0 },
                };
            }

            // -------- Weather --------
            List<WeatherData> weatherData = new List<WeatherData>();
            try
            {
                string weatherUrl = $"http://api.weatherapi.com/v1/current.json?key={_weatherApiKey}&q=Kirklareli&aqi=no";
                var response = await _httpClient.GetStringAsync(weatherUrl);
                var json = JObject.Parse(response);

                weatherData.Add(new WeatherData
                {
                    City = (string)json["location"]["name"],
                    Date = (string)json["location"]["localtime"],
                    TemperatureC = (double)json["current"]["temp_c"],
                    Condition = (string)json["current"]["condition"]["text"],
                    WindKph = (double)json["current"]["wind_kph"],
                    Humidity = (int)json["current"]["humidity"]
                });
            }
            catch
            {
                weatherData.Add(new WeatherData
                {
                    City = "Weather API çalışmıyor",
                    Date = "-",
                    TemperatureC = 0.0,
                    Condition = "-",
                    WindKph = 0.0,
                    Humidity = 0
                });
            }

            // -------- Exchange Rate --------
            decimal usdToTry = 0;
            try
            {
                string exchangeUrl = $"https://v6.exchangerate-api.com/v6/{_exchangeApiKey}/latest/USD";
                var exchangeResponse = await _httpClient.GetStringAsync(exchangeUrl);
                var json = JObject.Parse(exchangeResponse);
                usdToTry = (decimal)json["conversion_rates"]["TRY"];
            }
            catch
            {
                usdToTry = 0; // API çalışmazsa 0 olarak kalacak
            }

            var dashboardModel = new DashboardViewModel
            {
                FuelPrices = fuelData,
                WeatherData = weatherData,
                EmployeesCount = _context.Employees.Count(),
                DistanceCount = _context.Distances.Count(),
                ReservationsCount = _context.Reservations.Count(),
                CarCount = _context.Cars.Count(),
                UsdToTry = usdToTry
            };

            return View("Index", dashboardModel);
        }
    }
}
