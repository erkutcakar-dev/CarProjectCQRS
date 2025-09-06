using CarProjectCQRS.CQRSPattern.Commands.TurkeyAirportCommands;
using CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers;
using CarProjectCQRS.CQRSPattern.Queries.TurkeyAirportQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class TurkeyAirportController : Controller
    {
        private readonly GetTurkeyAirportQueryHandler _getTurkeyAirportQueryHandler;
        private readonly GetTurkeyAirportByIdQueryHandler _getTurkeyAirportByIdQueryHandler;
        private readonly CreateTurkeyAirportCommandHandler _createTurkeyAirportCommandHandler;
        private readonly UpdateTurkeyAirportCommandHandler _updateTurkeyAirportCommandHandler;
        private readonly RemoveTurkeyAirportCommandHandler _removeTurkeyAirportCommandHandler;

        public TurkeyAirportController(
            GetTurkeyAirportQueryHandler getTurkeyAirportQueryHandler,
            GetTurkeyAirportByIdQueryHandler getTurkeyAirportByIdQueryHandler,
            CreateTurkeyAirportCommandHandler createTurkeyAirportCommandHandler,
            UpdateTurkeyAirportCommandHandler updateTurkeyAirportCommandHandler,
            RemoveTurkeyAirportCommandHandler removeTurkeyAirportCommandHandler)
        {
            _getTurkeyAirportQueryHandler = getTurkeyAirportQueryHandler;
            _getTurkeyAirportByIdQueryHandler = getTurkeyAirportByIdQueryHandler;
            _createTurkeyAirportCommandHandler = createTurkeyAirportCommandHandler;
            _updateTurkeyAirportCommandHandler = updateTurkeyAirportCommandHandler;
            _removeTurkeyAirportCommandHandler = removeTurkeyAirportCommandHandler;
        }

        // Ana liste sayfası - TurkeyAirportList.cshtml ile uyumlu
        public async Task<IActionResult> TurkeyAirportList()
        {
            try
            {
                var values = await _getTurkeyAirportQueryHandler.Handle();
                return View(values.Select(x => new TurkeyAirport
                {
                    AirPortId = x.AirPortId,
                    Province = x.Province,
                    AirportName = x.AirportName
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the turkey airport list.";
                return View(new List<TurkeyAirport>());
            }
        }

        // Yeni ekleme sayfası - AddTurkeyAirport.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddTurkeyAirport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTurkeyAirport(TurkeyAirport turkeyAirport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateTurkeyAirportCommands
                    {
                        Province = turkeyAirport.Province,
                        AirportName = turkeyAirport.AirportName
                    };

                    await _createTurkeyAirportCommandHandler.Handle(command);
                    TempData["Success"] = "Turkey Airport has been successfully created.";
                    return RedirectToAction("TurkeyAirportList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the turkey airport.";
                }
            }
            return View(turkeyAirport);
        }

        // Düzenleme sayfası - EditTurkeyAirport.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditTurkeyAirport(int id)
        {
            try
            {
                var value = await _getTurkeyAirportByIdQueryHandler.Handle(new GetTurkeyAirportByIdQueries(id));
                if (value == null)
                    return NotFound();

                var turkeyAirport = new TurkeyAirport
                {
                    AirPortId = value.AirPortId,
                    Province = value.Province,
                    AirportName = value.AirportName
                };

                return View(turkeyAirport);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the turkey airport information.";
                return RedirectToAction("TurkeyAirportList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTurkeyAirport(TurkeyAirport turkeyAirport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateTurkeyAirportCommands
                    {
                        AirPortId = turkeyAirport.AirPortId,
                        Province = turkeyAirport.Province,
                        AirportName = turkeyAirport.AirportName
                    };

                    await _updateTurkeyAirportCommandHandler.Handle(command);
                    TempData["Success"] = "Turkey Airport has been successfully updated.";
                    return RedirectToAction("TurkeyAirportList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the turkey airport.";
                }
            }
            return View(turkeyAirport);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteTurkeyAirport(int id)
        {
            try
            {
                await _removeTurkeyAirportCommandHandler.Handle(new RemoveTurkeyAirportCommands(id));
                TempData["Success"] = "Turkey Airport has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the turkey airport.";
            }
            return RedirectToAction("TurkeyAirportList");
        }
    }
}