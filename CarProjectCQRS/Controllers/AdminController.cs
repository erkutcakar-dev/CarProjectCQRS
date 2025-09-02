using CarProjectCQRS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace CarProjectCQRS.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "pYg323LIQgLqTXBozpGHkGJSgbhkw3XOFkmdAp2N"; // EIA API key

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // EIA API çağrısı
                string url = $"https://api.eia.gov/v2/petroleum/pri/gnd/data/?api_key={_apiKey}&frequency=weekly&data[0]=value&facets[product][]=EPMRU&sort[0][column]=period&sort[0][direction]=desc&offset=0&length=1";

                Console.WriteLine($"API URL: {url}");

                var result = await _httpClient.GetFromJsonAsync<EiaGasPriceResponse>(url);

                Console.WriteLine($"API Response: {result?.response?.data?.Count ?? 0} records");

                var fuelData = result?.response?.data ?? new List<EiaDataPoint>();

                // FuelPrice verisini ViewData yerine model olarak gönderebiliriz
                return View(fuelData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                // Hata durumunda test verisi döndür
                var testData = new List<EiaDataPoint>
                {
                    new EiaDataPoint { period = "2024-01-15", value = 3.245m },
                    new EiaDataPoint { period = "2024-01-08", value = 3.189m }
                };
                return View(testData);
            }
        }
    }
}
