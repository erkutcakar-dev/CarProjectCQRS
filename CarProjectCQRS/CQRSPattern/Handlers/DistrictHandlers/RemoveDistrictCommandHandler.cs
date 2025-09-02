using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistrictCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictHandlers
{
    public class RemoveDistrictCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveDistrictCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveDistrictCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.DistrictId <= 0)
                    throw new ArgumentException("Invalid District ID provided", nameof(commands.DistrictId));

                var values = await _context.Districts.FindAsync(commands.DistrictId);
                
                if (values == null)
                    throw new KeyNotFoundException($"District record with ID {commands.DistrictId} not found");

                _context.Districts.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the district record", ex);
            }
        }
    }
}


