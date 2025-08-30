using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ReservationCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers
{
    public class RemoveReservationCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveReservationCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveReservationCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.ReservationId <= 0)
                    throw new ArgumentException("Invalid Reservation ID provided", nameof(commands.ReservationId));

                var values = await _context.Reservations.FindAsync(commands.ReservationId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Reservation record with ID {commands.ReservationId} not found");

                _context.Reservations.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the reservation record", ex);
            }
        }
    }
}
