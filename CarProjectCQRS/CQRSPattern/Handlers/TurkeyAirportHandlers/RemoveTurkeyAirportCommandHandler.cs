using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.TurkeyAirportCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers
{
    public class RemoveTurkeyAirportCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveTurkeyAirportCommandHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(RemoveTurkeyAirportCommands command)
        {
            try
            {
                if (command == null)
                    throw new ArgumentNullException(nameof(command), "Remove command cannot be null");

                if (command.AirPortId <= 0)
                    throw new ArgumentException("Invalid Airport ID provided", nameof(command.AirPortId));

                var airport = await _context.TurkeyAirports.FindAsync(command.AirPortId);
                
                if (airport == null)
                    throw new KeyNotFoundException($"Airport with ID {command.AirPortId} not found");

                _context.TurkeyAirports.Remove(airport);
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
                throw new InvalidOperationException("An error occurred while removing the airport record", ex);
            }
        }
    }
}

