using CarProjectCQRS.CQRSPattern.Commands.ReservationCommands;
using CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers;
using CarProjectCQRS.CQRSPattern.Queries.ReservationQueries;
using CarProjectCQRS.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarProjectCQRS.Controllers
{
    public class ReservationController : Controller
    {
        private readonly GetReservationQueryHandler _getReservationQueryHandler;
        private readonly GetReservationByIdQueryHandler _getReservationByIdQueryHandler;
        private readonly CreateReservationCommandHandler _createReservationCommandHandler;
        private readonly UpdateReservationCommandHandler _updateReservationCommandHandler;
        private readonly RemoveReservationCommandHandler _removeReservationCommandHandler;

        public ReservationController(
            GetReservationQueryHandler getReservationQueryHandler,
            GetReservationByIdQueryHandler getReservationByIdQueryHandler,
            CreateReservationCommandHandler createReservationCommandHandler,
            UpdateReservationCommandHandler updateReservationCommandHandler,
            RemoveReservationCommandHandler removeReservationCommandHandler)
        {
            _getReservationQueryHandler = getReservationQueryHandler;
            _getReservationByIdQueryHandler = getReservationByIdQueryHandler;
            _createReservationCommandHandler = createReservationCommandHandler;
            _updateReservationCommandHandler = updateReservationCommandHandler;
            _removeReservationCommandHandler = removeReservationCommandHandler;
        }

        // Ana liste sayfası - ReservationList.cshtml ile uyumlu
        public async Task<IActionResult> ReservationList()
        {
            try
            {
                var values = await _getReservationQueryHandler.Handle();
                return View(values.Select(x => new Reservation
                {
                    ReservationId = x.ReservationId,
                    CarId = x.CarId,
                    CarType = x.CarType,
                    PickUpLocation = x.PickUpLocation,
                    DropOffLocation = x.DropOffLocation,
                    airport = x.airport,
                    PickUpDate = x.PickUpDate,
                    DropOffDate = x.DropOffDate,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate
                }).ToList());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the reservation list.";
                return View(new List<Reservation>());
            }
        }

        // Yeni ekleme sayfası - AddReservation.cshtml ile uyumlu
        [HttpGet]
        public IActionResult AddReservation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new CreateReservationCommands
                    {
                        CarId = reservation.CarId,
                        CarType = reservation.CarType,
                        PickUpLocation = reservation.PickUpLocation,
                        DropOffLocation = reservation.DropOffLocation,
                        airport = reservation.airport,
                        PickUpDate = reservation.PickUpDate,
                        DropOffDate = reservation.DropOffDate,
                        IsActive = reservation.IsActive
                    };

                    await _createReservationCommandHandler.Handle(command);
                    TempData["Success"] = "Reservation has been successfully created.";
                    return RedirectToAction("ReservationList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the reservation.";
                }
            }
            return View(reservation);
        }

        // Düzenleme sayfası - EditReservation.cshtml ile uyumlu
        [HttpGet]
        public async Task<IActionResult> EditReservation(int id)
        {
            try
            {
                var value = await _getReservationByIdQueryHandler.Handle(new GetReservationByIdQueries(id));
                if (value == null)
                    return NotFound();

                var reservation = new Reservation
                {
                    ReservationId = value.ReservationId,
                    CarId = value.CarId,
                    CarType = value.CarType,
                    PickUpLocation = value.PickUpLocation,
                    DropOffLocation = value.DropOffLocation,
                    airport = value.airport,
                    PickUpDate = value.PickUpDate,
                    DropOffDate = value.DropOffDate,
                    IsActive = value.IsActive,
                    CreatedDate = value.CreatedDate
                };

                return View(reservation);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the reservation information.";
                return RedirectToAction("ReservationList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new UpdateReservationCommands
                    {
                        ReservationId = reservation.ReservationId,
                        CarId = reservation.CarId,
                        CarType = reservation.CarType,
                        PickUpLocation = reservation.PickUpLocation,
                        DropOffLocation = reservation.DropOffLocation,
                        airport = reservation.airport,
                        PickUpDate = reservation.PickUpDate,
                        DropOffDate = reservation.DropOffDate,
                        IsActive = reservation.IsActive
                    };

                    await _updateReservationCommandHandler.Handle(command);
                    TempData["Success"] = "Reservation has been successfully updated.";
                    return RedirectToAction("ReservationList");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the reservation.";
                }
            }
            return View(reservation);
        }

        // Silme işlemi
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _removeReservationCommandHandler.Handle(new RemoveReservationCommands(id));
                TempData["Success"] = "Reservation has been successfully deleted.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the reservation.";
            }
            return RedirectToAction("ReservationList");
        }

        // Aktif/Pasif durumu değiştirme
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var value = await _getReservationByIdQueryHandler.Handle(new GetReservationByIdQueries(id));
                if (value == null)
                    return NotFound();

                var command = new UpdateReservationCommands
                {
                    ReservationId = value.ReservationId,
                    CarId = value.CarId,
                    CarType = value.CarType,
                    PickUpLocation = value.PickUpLocation,
                    DropOffLocation = value.DropOffLocation,
                    airport = value.airport,
                    PickUpDate = value.PickUpDate,
                    DropOffDate = value.DropOffDate,
                    IsActive = !value.IsActive  // Toggle the status
                };

                await _updateReservationCommandHandler.Handle(command);
                
                string statusText = command.IsActive ? "activated" : "deactivated";
                TempData["Success"] = $"Reservation has been successfully {statusText}.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the status.";
            }
            return RedirectToAction("ReservationList");
        }
    }
}
