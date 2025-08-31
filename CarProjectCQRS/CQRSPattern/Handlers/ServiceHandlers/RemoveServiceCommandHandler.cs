using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ServiceCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers
{
    public class RemoveServiceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveServiceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveServiceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.ServiceId <= 0)
                    throw new ArgumentException("Invalid Service ID provided", nameof(commands.ServiceId));

                var values = await _context.Services.FindAsync(commands.ServiceId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Service record with ID {commands.ServiceId} not found");

                _context.Services.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the service record", ex);
            }
        }
    }
}

