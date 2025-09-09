using CarProjectCQRS.Models;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers;
using CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers;
using CarProjectCQRS.CQRSPattern.Commands.ReservationCommands;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class BookingController : Controller
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetDistanceQueryHandler _getDistanceQueryHandler;
        private readonly GetTurkeyAirportQueryHandler _getTurkeyAirportQueryHandler;
        private readonly CreateReservationCommandHandler _createReservationCommandHandler;

        public BookingController(
            GetCarQueryHandler getCarQueryHandler,
            GetDistanceQueryHandler getDistanceQueryHandler,
            GetTurkeyAirportQueryHandler getTurkeyAirportQueryHandler,
            CreateReservationCommandHandler createReservationCommandHandler)
        {
            _getCarQueryHandler = getCarQueryHandler;
            _getDistanceQueryHandler = getDistanceQueryHandler;
            _getTurkeyAirportQueryHandler = getTurkeyAirportQueryHandler;
            _createReservationCommandHandler = createReservationCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Search(CarBookingSearchModel model)
        {
            try
            {
                // Tüm araçları getir
                var allCars = await _getCarQueryHandler.Handle();
                
                // Seçilen araç tipine göre filtrele (eğer seçilmişse)
                var filteredCars = allCars.ToList(); // IsAvailable filtresini kaldırdık
                
                if (model.CarId > 0)
                {
                    // Belirli bir araç seçilmişse o markaya ait tüm araçları göster
                    var selectedCarType = allCars.FirstOrDefault(c => c.CarId == model.CarId);
                    if (selectedCarType != null)
                    {
                        // Sadece Brand'e göre filtrele (aynı markaya ait tüm araçlar)
                        filteredCars = filteredCars.Where(c => 
                            c.Brand == selectedCarType.Brand).ToList();
                    }
                }
                else
                {
                    // Araç seçilmemişse tüm araçları göster
                    filteredCars = allCars.ToList();
                }

                // Mesafe hesapla
                var distances = await _getDistanceQueryHandler.Handle();
                var distance = distances.FirstOrDefault(d => 
                    (d.From == model.PickUpLocation && d.Destination == model.DropOffLocation) ||
                    (d.From == model.DropOffLocation && d.Destination == model.PickUpLocation));

                if (distance != null)
                {
                    model.Distance = distance.DistanceValue;
                }

                // Toplam gün sayısını hesapla
                var totalDays = (model.DropOffDate - model.PickUpDate).Days;
                if (totalDays <= 0) totalDays = 1; // Minimum 1 gün

                // Toplam fiyatı hesapla
                var totalPrice = filteredCars.Sum(c => c.DailyPrice * totalDays);

                // Seçilen araç tipinin bilgilerini model'e ekle
                if (model.CarId > 0)
                {
                    var selectedCar = allCars.FirstOrDefault(c => c.CarId == model.CarId);
                    if (selectedCar != null)
                    {
                        model.Brand = selectedCar.Brand;
                        model.Model = selectedCar.Model;
                    }
                }

                var viewModel = new AvailableCarsViewModel
                {
                    AvailableCars = filteredCars.Select(x => new Car
                    {
                        CarId = x.CarId,
                        Brand = x.Brand,
                        Model = x.Model,
                        ImageUrl = x.ImageUrl,
                        ReviewScore = x.ReviewScore,
                        DailyPrice = x.DailyPrice,
                        SeatCount = x.SeatCount,
                        TransmissionType = x.TransmissionType,
                        FuelType = x.FuelType,
                        ModelYear = x.ModelYear,
                        GearType = x.GearType,
                        Mileage = x.Mileage,
                        IsAvailable = x.IsAvailable,
                        CreatedDate = x.CreatedDate
                    }).ToList(),
                    SearchCriteria = model,
                    TotalDays = totalDays,
                    TotalPrice = totalPrice
                };

                return View("AvailableCars", viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while searching for available cars.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AvailableCars(int? carId = null, string pickUpLocation = null, string dropOffLocation = null, DateTime? pickUpDate = null, DateTime? dropOffDate = null, int? airportId = null)
        {
            try
            {
                var allCars = await _getCarQueryHandler.Handle();
                var availableCars = allCars.Where(c => c.IsAvailable).ToList();

                // Eğer parametreler varsa filtreleme yap
                if (carId.HasValue && carId > 0)
                {
                    var selectedCarType = allCars.FirstOrDefault(c => c.CarId == carId);
                    if (selectedCarType != null)
                    {
                        availableCars = availableCars.Where(c => c.Brand == selectedCarType.Brand).ToList();
                    }
                }

                var totalDays = 1;
                if (pickUpDate.HasValue && dropOffDate.HasValue)
                {
                    totalDays = (dropOffDate.Value - pickUpDate.Value).Days;
                    if (totalDays <= 0) totalDays = 1;
                }

                var searchCriteria = new CarBookingSearchModel
                {
                    CarId = carId ?? 0,
                    PickUpLocation = pickUpLocation ?? "",
                    DropOffLocation = dropOffLocation ?? "",
                    PickUpDate = pickUpDate ?? DateTime.Now,
                    DropOffDate = dropOffDate ?? DateTime.Now.AddDays(1),
                    AirportId = airportId
                };

                var viewModel = new AvailableCarsViewModel
                {
                    AvailableCars = availableCars.Select(x => new Car
                    {
                        CarId = x.CarId,
                        Brand = x.Brand,
                        Model = x.Model,
                        ImageUrl = x.ImageUrl,
                        ReviewScore = x.ReviewScore,
                        DailyPrice = x.DailyPrice,
                        SeatCount = x.SeatCount,
                        TransmissionType = x.TransmissionType,
                        FuelType = x.FuelType,
                        ModelYear = x.ModelYear,
                        GearType = x.GearType,
                        Mileage = x.Mileage,
                        IsAvailable = x.IsAvailable,
                        CreatedDate = x.CreatedDate
                    }).ToList(),
                    SearchCriteria = searchCriteria,
                    TotalDays = totalDays,
                    TotalPrice = availableCars.Sum(c => c.DailyPrice * totalDays)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading available cars.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(CarBookingSearchModel model)
        {
            try
            {
                // Araç bilgilerini al
                var cars = await _getCarQueryHandler.Handle();
                var selectedCar = cars.FirstOrDefault(c => c.CarId == model.CarId);
                
                if (selectedCar == null)
                {
                    TempData["Error"] = "Selected car not found.";
                    return RedirectToAction("AvailableCars");
                }

                // Havalimanı bilgisini al
                string airportName = "";
                if (model.AirportId.HasValue && model.AirportId > 0)
                {
                    var airports = await _getTurkeyAirportQueryHandler.Handle();
                    var selectedAirport = airports.FirstOrDefault(a => a.AirPortId == model.AirportId);
                    airportName = selectedAirport?.AirportName ?? "";
                }

                // Rezervasyon kaydı oluştur
                var reservationCommand = new CreateReservationCommands
                {
                    CarId = model.CarId,
                    CarType = $"{selectedCar.Brand} {selectedCar.Model}",
                    PickUpLocation = model.PickUpLocation,
                    DropOffLocation = model.DropOffLocation,
                    airport = airportName,
                    PickUpDate = model.PickUpDate,
                    DropOffDate = model.DropOffDate,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                await _createReservationCommandHandler.Handle(reservationCommand);

                TempData["Success"] = "Reservation successful!";
                
                // BookingConfirmation sayfasına geri dön (Sweet Alert için)
                return RedirectToAction("BookingConfirmation", new { 
                    carId = model.CarId,
                    pickUpLocation = model.PickUpLocation,
                    dropOffLocation = model.DropOffLocation,
                    pickUpDate = model.PickUpDate,
                    dropOffDate = model.DropOffDate,
                    airportId = model.AirportId
                });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while confirming the booking: {ex.Message}";
                return RedirectToAction("AvailableCars");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BookingConfirmation(int carId, string pickUpLocation, string dropOffLocation, DateTime pickUpDate, DateTime dropOffDate, int? airportId = null)
        {
            try
            {
                var car = await _getCarQueryHandler.Handle();
                var selectedCar = car.FirstOrDefault(c => c.CarId == carId);
                
                if (selectedCar == null)
                {
                    TempData["Error"] = "Selected car not found.";
                    return RedirectToAction("AvailableCars");
                }

                var totalDays = (dropOffDate - pickUpDate).Days;
                if (totalDays <= 0) totalDays = 1;

                var viewModel = new AvailableCarsViewModel
                {
                    AvailableCars = new List<Car>
                    {
                        new Car
                        {
                            CarId = selectedCar.CarId,
                            Brand = selectedCar.Brand,
                            Model = selectedCar.Model,
                            ImageUrl = selectedCar.ImageUrl,
                            ReviewScore = selectedCar.ReviewScore,
                            DailyPrice = selectedCar.DailyPrice,
                            SeatCount = selectedCar.SeatCount,
                            TransmissionType = selectedCar.TransmissionType,
                            FuelType = selectedCar.FuelType,
                            ModelYear = selectedCar.ModelYear,
                            GearType = selectedCar.GearType,
                            Mileage = selectedCar.Mileage,
                            IsAvailable = selectedCar.IsAvailable,
                            CreatedDate = selectedCar.CreatedDate
                        }
                    },
                    SearchCriteria = new CarBookingSearchModel
                    {
                        CarId = carId,
                        PickUpLocation = pickUpLocation,
                        DropOffLocation = dropOffLocation,
                        PickUpDate = pickUpDate,
                        DropOffDate = dropOffDate,
                        AirportId = airportId
                    },
                    TotalDays = totalDays,
                    TotalPrice = selectedCar.DailyPrice * totalDays
                };

                return View("BookingConfirmation", viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading booking confirmation.";
                return RedirectToAction("AvailableCars");
            }


        }
    }
}
