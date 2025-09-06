using CarProjectCQRS.CQRSPattern.Commands.DistanceCommands;
using CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers;
using CarProjectCQRS.CQRSPattern.Queries.DistanceQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class DistanceController : Controller
    {
        private readonly GetDistanceQueryHandler _getDistanceQueryHandler;
        private readonly GetDistanceByIdQueryHandler _getDistanceByIdQueryHandler;
        private readonly CreateDistanceCommandHandler _createDistanceCommandHandler;
        private readonly UpdateDistanceCommandHandler _updateDistanceCommandHandler;
        private readonly RemoveDistanceCommandHandler _removeDistanceCommandHandler;

        public DistanceController(
            GetDistanceQueryHandler getDistanceQueryHandler,
            GetDistanceByIdQueryHandler getDistanceByIdQueryHandler,
            CreateDistanceCommandHandler createDistanceCommandHandler,
            UpdateDistanceCommandHandler updateDistanceCommandHandler,
            RemoveDistanceCommandHandler removeDistanceCommandHandler)
        {
            _getDistanceQueryHandler = getDistanceQueryHandler;
            _getDistanceByIdQueryHandler = getDistanceByIdQueryHandler;
            _createDistanceCommandHandler = createDistanceCommandHandler;
            _updateDistanceCommandHandler = updateDistanceCommandHandler;
            _removeDistanceCommandHandler = removeDistanceCommandHandler;
        }

        // Ana liste sayfası - DistanceList.cshtml ile uyumlu
        public async Task<IActionResult> DistanceList()
        {
            try
            {
                var values = await _getDistanceQueryHandler.Handle();
                return View(values.Select(x => new Distance
                {
                    DistanceId = x.DistanceId,
                    From = x.From,
                    Destination = x.Destination,
                    DistanceValue = x.DistanceValue
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the distance list.";
                return View(new List<Distance>());
            }
        }

        // Yeni ekleme sayfası - AddDistance.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddDistance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDistance(Distance distance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateDistanceCommands
                    {
                        From = distance.From,
                        Destination = distance.Destination,
                        DistanceValue = distance.DistanceValue
                    };

                    await _createDistanceCommandHandler.Handle(command);
                    TempData["Success"] = "Distance information has been successfully created.";
                    return RedirectToAction("DistanceList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the distance information.";
                }
            }
            return View(distance);
        }

        // Düzenleme sayfası - EditDistance.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditDistance(int id)
        {
            try
            {
                var value = await _getDistanceByIdQueryHandler.Handle(new GetDistanceByIdQuery(id));
                if (value == null)
                    return NotFound();

                var distance = new Distance
                {
                    DistanceId = value.DistanceId,
                    From = value.From,
                    Destination = value.Destination,
                    DistanceValue = value.DistanceValue
                };

                return View(distance);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the distance information.";
                return RedirectToAction("DistanceList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDistance(Distance distance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateDistanceCommands
                    {
                        DistanceId = distance.DistanceId,
                        From = distance.From,
                        Destination = distance.Destination,
                        DistanceValue = distance.DistanceValue
                    };

                    await _updateDistanceCommandHandler.Handle(command);
                    TempData["Success"] = "Distance information has been successfully updated.";
                    return RedirectToAction("DistanceList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the distance information.";
                }
            }
            return View(distance);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteDistance(int id)
        {
            try
            {
                await _removeDistanceCommandHandler.Handle(new RemoveDistanceCommands(id));
                TempData["Success"] = "Distance information has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the distance information.";
            }
            return RedirectToAction("DistanceList");
        }
    }
}

