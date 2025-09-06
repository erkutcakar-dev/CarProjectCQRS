
using Microsoft.AspNetCore.Mvc;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers;
using CarProjectCQRS.CQRSPattern.Results.Car;
using CarProjectCQRS.CQRSPattern.Results.TurkeyAirport;
using CarProjectCQRS.CQRSPattern.Results.Distance;
using CarProjectCQRS.Models;


namespace CarProjectCQRS.ViewComponents.MainUiViewComponents
{
    public class CarouselComponentPartial : ViewComponent
    {
        private readonly GetCarQueryHandler _carHandler;
        private readonly GetTurkeyAirportQueryHandler _airportHandler;
        private readonly GetDistanceQueryHandler _distanceHandler;

        public CarouselComponentPartial(
            GetCarQueryHandler carHandler,
            GetTurkeyAirportQueryHandler airportHandler,
            GetDistanceQueryHandler distanceHandler)
        {
            _carHandler = carHandler;
            _airportHandler = airportHandler;
            _distanceHandler = distanceHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var cars = await _carHandler.Handle();
                var airports = await _airportHandler.Handle();
                var distances = await _distanceHandler.Handle();

                // Debug için log ekleyelim
                Console.WriteLine($"Cars count: {cars?.Count ?? 0}");
                Console.WriteLine($"Airports count: {airports?.Count ?? 0}");
                Console.WriteLine($"Distances count: {distances?.Count ?? 0}");

                var model = new CarouselViewModel
                {
                    Cars = cars ?? new List<GetCarQueryResult>(),
                    Airports = airports ?? new List<GetTurkeyAirportQueryResult>(),
                    Distances = distances ?? new List<GetDistanceQueryResult>()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CarouselComponentPartial Error: {ex.Message}");
                // Hata durumunda boş model döndür
                return View(new CarouselViewModel());
            }
        }
    }
}
