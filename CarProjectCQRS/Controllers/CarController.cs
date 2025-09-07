using CarProjectCQRS.CQRSPattern.Commands.CarCommands;
using CarProjectCQRS.CQRSPattern.Handlers.CarHandlers;
using CarProjectCQRS.CQRSPattern.Queries.CarQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class CarController : Controller
    {
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly CreateCarCommandHandler _createCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly RemoveCarCommandHandler _removeCarCommandHandler;

        public CarController(
            GetCarQueryHandler getCarQueryHandler,
            GetCarByIdQueryHandler getCarByIdQueryHandler,
            CreateCarCommandHandler createCarCommandHandler,
            UpdateCarCommandHandler updateCarCommandHandler,
            RemoveCarCommandHandler removeCarCommandHandler)
        {
            _getCarQueryHandler = getCarQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _createCarCommandHandler = createCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _removeCarCommandHandler = removeCarCommandHandler;
        }

        // Ana liste sayfası - CarList.cshtml ile uyumlu
        public async Task<IActionResult> CarList()
        {
            try
            {
                var values = await _getCarQueryHandler.Handle();
                return View(values.Select(x => new Car
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
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the car list.";
                return View(new List<Car>());
            }
        }

        // Yeni ekleme sayfası - AddCar.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateCarCommands
                    {
                        Brand = car.Brand,
                        Model = car.Model,
                        ImageUrl = car.ImageUrl,
                        ReviewScore = car.ReviewScore,
                        DailyPrice = car.DailyPrice,
                        SeatCount = car.SeatCount,
                        TransmissionType = car.TransmissionType,
                        FuelType = car.FuelType,
                        ModelYear = car.ModelYear,
                        GearType = car.GearType,
                        Mileage = car.Mileage,
                        IsAvailable = car.IsAvailable
                    };

                    await _createCarCommandHandler.Handle(command);
                    TempData["Success"] = "Car information has been successfully created.";
                    return RedirectToAction("CarList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the car information.";
                }
            }
            return View(car);
        }

        // Düzenleme sayfası - EditCar.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditCar(int id)
        {
            try
            {
                var value = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));
                if (value == null)
                    return NotFound();

                var car = new Car
                {
                    CarId = value.CarId,
                    Brand = value.Brand,
                    Model = value.Model,
                    ImageUrl = value.ImageUrl,
                    ReviewScore = value.ReviewScore,
                    DailyPrice = value.DailyPrice,
                    SeatCount = value.SeatCount,
                    TransmissionType = value.TransmissionType,
                    FuelType = value.FuelType,
                    ModelYear = value.ModelYear,
                    GearType = value.GearType,
                    Mileage = value.Mileage,
                    IsAvailable = value.IsAvailable,
                    CreatedDate = value.CreatedDate
                };

                return View(car);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the car information.";
                return RedirectToAction("CarList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateCarCommands
                    {
                        CarId = car.CarId,
                        Brand = car.Brand,
                        Model = car.Model,
                        ImageUrl = car.ImageUrl,
                        ReviewScore = car.ReviewScore,
                        DailyPrice = car.DailyPrice,
                        SeatCount = car.SeatCount,
                        TransmissionType = car.TransmissionType,
                        FuelType = car.FuelType,
                        ModelYear = car.ModelYear,
                        GearType = car.GearType,
                        Mileage = car.Mileage,
                        IsAvailable = car.IsAvailable
                    };

                    await _updateCarCommandHandler.Handle(command);
                    TempData["Success"] = "Car information has been successfully updated.";
                    return RedirectToAction("CarList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the car information.";
                }
            }
            return View(car);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _removeCarCommandHandler.Handle(new RemoveCarCommands(id));
                TempData["Success"] = "Car information has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the car information.";
            }
            return RedirectToAction("CarList");
        }

        // Müsait/Müsait Değil durumu değiştirme
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            try
            {
                var value = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateCarCommands
                {
                    CarId = value.CarId,
                    Brand = value.Brand,
                    Model = value.Model,
                    ImageUrl = value.ImageUrl,
                    ReviewScore = value.ReviewScore,
                    DailyPrice = value.DailyPrice,
                    SeatCount = value.SeatCount,
                    TransmissionType = value.TransmissionType,
                    FuelType = value.FuelType,
                    ModelYear = value.ModelYear,
                    GearType = value.GearType,
                    Mileage = value.Mileage,
                    IsAvailable = !value.IsAvailable  // Toggle the availability
                };

                await _updateCarCommandHandler.Handle(command);
                
                string statusText = command.IsAvailable ? "made available" : "made unavailable";
                TempData["Success"] = $"Car has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the availability status.";
            }
            return RedirectToAction("CarList");
        }

        // Test verileri ekleme (geliştirme amaçlı)
        public async Task<IActionResult> AddTestData()
        {
            try
            {
                var testCars = new List<CreateCarCommands>
                {
                    new CreateCarCommands
                    {
                        Brand = "Toyota",
                        Model = "Corolla",
                        ImageUrl = "/Cental-1.0.0/img/car-1.png",
                        ReviewScore = 4.5,
                        DailyPrice = 50.00m,
                        SeatCount = 5,
                        TransmissionType = "Automatic",
                        FuelType = "Gasoline",
                        ModelYear = 2020,
                        GearType = "Manual",
                        Mileage = 20000,
                        IsAvailable = true
                    },
                    new CreateCarCommands
                    {
                        Brand = "Honda",
                        Model = "Civic",
                        ImageUrl = "/Cental-1.0.0/img/car-2.png",
                        ReviewScore = 4.3,
                        DailyPrice = 55.00m,
                        SeatCount = 5,
                        TransmissionType = "Manual",
                        FuelType = "Gasoline",
                        ModelYear = 2021,
                        GearType = "Automatic",
                        Mileage = 15000,
                        IsAvailable = true
                    },
                    new CreateCarCommands
                    {
                        Brand = "Ford",
                        Model = "Focus",
                        ImageUrl = "/Cental-1.0.0/img/car-3.png",
                        ReviewScore = 4.6,
                        DailyPrice = 60.00m,
                        SeatCount = 5,
                        TransmissionType = "Automatic",
                        FuelType = "Diesel",
                        ModelYear = 2019,
                        GearType = "Manual",
                        Mileage = 30000,
                        IsAvailable = true
                    },
                    new CreateCarCommands
                    {
                        Brand = "BMW",
                        Model = "3 Series",
                        ImageUrl = "/Cental-1.0.0/img/car-4.png",
                        ReviewScore = 4.8,
                        DailyPrice = 120.00m,
                        SeatCount = 5,
                        TransmissionType = "Automatic",
                        FuelType = "Gasoline",
                        ModelYear = 2022,
                        GearType = "Automatic",
                        Mileage = 10000,
                        IsAvailable = true
                    },
                    new CreateCarCommands
                    {
                        Brand = "Mercedes",
                        Model = "C-Class",
                        ImageUrl = "/Cental-1.0.0/img/car-1.png",
                        ReviewScore = 4.7,
                        DailyPrice = 150.00m,
                        SeatCount = 5,
                        TransmissionType = "Automatic",
                        FuelType = "Gasoline",
                        ModelYear = 2023,
                        GearType = "Automatic",
                        Mileage = 5000,
                        IsAvailable = true
                    }
                };

                foreach (var car in testCars)
                {
                    await _createCarCommandHandler.Handle(car);
                }

                TempData["Success"] = "Test car data has been successfully added.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding test data.";
            }
            return RedirectToAction("CarList");
        }

    }
}

