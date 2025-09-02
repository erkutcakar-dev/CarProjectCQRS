using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ReservationCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateReservationCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateReservationCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Reservation command cannot be null");

                _context.Reservations.Add(new Reservation()
                {
                    CarId = commands.CarId,
                    CarType = commands.CarType,
                    PickUpLocation = commands.PickUpLocation,
                    DropOffLocation = commands.DropOffLocation,
                    airport = commands.airport,
                    PickUpDate = commands.PickUpDate,
                    DropOffDate = commands.DropOffDate,
                    IsActive = commands.IsActive,
                    CreatedDate = DateTime.Now,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the reservation record", ex);
            }
        }
    }
}


