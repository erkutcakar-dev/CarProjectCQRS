using Microsoft.AspNetCore.Mvc;
using CarProjectCQRS.Context;
using CarProjectCQRS.Services; 
using CarProjectCQRS.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.Controllers
{
    public class AIController : Controller
    {
        private readonly AIService _aiService;
        private readonly CarProjectDbContext _context;

        public AIController(CarProjectDbContext context)
        {
            _aiService = new AIService();
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Tüm araçları getir
            var cars = await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
            
            // Filtreleme için dropdown verilerini hazırla
            ViewBag.Brands = cars.Select(c => c.Brand).Distinct().OrderBy(b => b).ToList();
            ViewBag.FuelTypes = cars.Select(c => c.FuelType).Distinct().OrderBy(f => f).ToList();
            ViewBag.GearTypes = cars.Select(c => c.GearType).Distinct().OrderBy(g => g).ToList();
            ViewBag.SeatCounts = cars.Select(c => c.SeatCount).Distinct().OrderBy(s => s).ToList();
            
            return View(cars);
        }

        [HttpPost]
        public async Task<IActionResult> SearchCars(string searchTerm, string brand, string fuelType, 
            string gearType, int? minSeats, int? maxSeats, decimal? minPrice, decimal? maxPrice, 
            int? minYear, int? maxYear, string sortBy = "Brand")
        {
            var query = _context.Cars.Where(c => c.IsAvailable).AsQueryable();

            // Arama terimi
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Brand.Contains(searchTerm) || c.Model.Contains(searchTerm));
            }

            // Marka filtresi
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(c => c.Brand == brand);
            }

            // Yakıt türü filtresi
            if (!string.IsNullOrEmpty(fuelType))
            {
                query = query.Where(c => c.FuelType == fuelType);
            }

            // Vites türü filtresi
            if (!string.IsNullOrEmpty(gearType))
            {
                query = query.Where(c => c.GearType == gearType);
            }

            // Koltuk sayısı filtresi
            if (minSeats.HasValue)
            {
                query = query.Where(c => c.SeatCount >= minSeats.Value);
            }
            if (maxSeats.HasValue)
            {
                query = query.Where(c => c.SeatCount <= maxSeats.Value);
            }

            // Fiyat filtresi
            if (minPrice.HasValue)
            {
                query = query.Where(c => c.DailyPrice >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(c => c.DailyPrice <= maxPrice.Value);
            }

            // Model yılı filtresi
            if (minYear.HasValue)
            {
                query = query.Where(c => c.ModelYear >= minYear.Value);
            }
            if (maxYear.HasValue)
            {
                query = query.Where(c => c.ModelYear <= maxYear.Value);
            }

            // Sıralama
            switch (sortBy)
            {
                case "PriceAsc":
                    query = query.OrderBy(c => c.DailyPrice);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(c => c.DailyPrice);
                    break;
                case "YearAsc":
                    query = query.OrderBy(c => c.ModelYear);
                    break;
                case "YearDesc":
                    query = query.OrderByDescending(c => c.ModelYear);
                    break;
                case "Rating":
                    query = query.OrderByDescending(c => c.ReviewScore);
                    break;
                default:
                    query = query.OrderBy(c => c.Brand).ThenBy(c => c.Model);
                    break;
            }

            var cars = await query.ToListAsync();

            // Filtreleme için dropdown verilerini hazırla
            var allCars = await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
            ViewBag.Brands = allCars.Select(c => c.Brand).Distinct().OrderBy(b => b).ToList();
            ViewBag.FuelTypes = allCars.Select(c => c.FuelType).Distinct().OrderBy(f => f).ToList();
            ViewBag.GearTypes = allCars.Select(c => c.GearType).Distinct().OrderBy(g => g).ToList();
            ViewBag.SeatCounts = allCars.Select(c => c.SeatCount).Distinct().OrderBy(s => s).ToList();

            // Filtreleme parametrelerini ViewBag'e gönder
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedBrand = brand;
            ViewBag.SelectedFuelType = fuelType;
            ViewBag.SelectedGearType = gearType;
            ViewBag.MinSeats = minSeats;
            ViewBag.MaxSeats = maxSeats;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinYear = minYear;
            ViewBag.MaxYear = maxYear;
            ViewBag.SortBy = sortBy;

            return View("Index", cars);
        }

        [HttpPost]
        public async Task<IActionResult> GetAIRecommendation(string requirements)
        {
            if (string.IsNullOrEmpty(requirements))
            {
                return Json(new { success = false, message = "Please provide your requirements." });
            }

            // Veritabanındaki araçları formatla
            var cars = string.Join(" | ", _context.Cars.Where(c => c.IsAvailable).Select(c =>
                $"{c.Brand} {c.Model} - Daily ${c.DailyPrice} - {c.FuelType} fuel - {c.TransmissionType} transmission - {c.SeatCount} seats - {c.ModelYear} model - {c.Mileage} km - Rating: {c.ReviewScore}"
            ).ToList());

            // Yapay zekadan öneri al
            var recommendation = await _aiService.GetCarRecommendationAsync(cars, requirements);

            return Json(new { success = true, recommendation = recommendation });
        }
    }
}
