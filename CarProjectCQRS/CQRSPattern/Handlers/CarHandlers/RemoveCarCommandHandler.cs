using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.CarCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.CarHandlers
{
    public class RemoveCarCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveCarCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCarCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.CarId <= 0)
                    throw new ArgumentException("Invalid Car ID provided", nameof(commands.CarId));

                var values = await _context.Cars.FindAsync(commands.CarId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Car record with ID {commands.CarId} not found");

                _context.Cars.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the car record", ex);
            }
        }
    }
}
