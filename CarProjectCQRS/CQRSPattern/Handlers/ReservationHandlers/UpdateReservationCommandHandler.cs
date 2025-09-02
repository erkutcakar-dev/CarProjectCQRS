using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ReservationCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers
{
    public class UpdateReservationCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateReservationCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateReservationCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.ReservationId <= 0)
                    throw new ArgumentException("Invalid Reservation ID provided", nameof(commands.ReservationId));

                var values = await _context.Reservations.FindAsync(commands.ReservationId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Reservation record with ID {commands.ReservationId} not found");

                values.CarId = commands.CarId;
                values.CarType = commands.CarType;
                values.PickUpLocation = commands.PickUpLocation;
                values.DropOffLocation = commands.DropOffLocation;
                values.airport = commands.airport;
                values.PickUpDate = commands.PickUpDate;
                values.DropOffDate = commands.DropOffDate;
                values.IsActive = commands.IsActive;
                values.CreatedDate = commands.CreatedDate;

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the reservation record", ex);
            }
        }
    }
}


