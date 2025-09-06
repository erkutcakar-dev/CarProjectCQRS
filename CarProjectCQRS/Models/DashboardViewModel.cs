using CarProjectCQRS.Models;
using System.Collections.Generic;

namespace CarProjectCQRS.Models
{
    public class DashboardViewModel
    {
        public List<EiaDataPoint> FuelPrices { get; set; } = new List<EiaDataPoint>();
        public List<WeatherData> WeatherData { get; set; } = new List<WeatherData>();
        public int EmployeesCount { get; set; }
        public int DistanceCount { get; set; }
        public int ReservationsCount { get; set; }
        public int CarCount { get; set; }
        public decimal UsdToTry { get; set; }

    }

    public class WeatherData
    {
        public string City { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public double TemperatureC { get; set; }
        public string Condition { get; set; } = string.Empty;
        public double WindKph { get; set; }
        public int Humidity { get; set; }
    }
    public class ExchangeRateResponse
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }




}