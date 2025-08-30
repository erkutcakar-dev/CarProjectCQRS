using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.AboutCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers
{
    public class RemoveAboutCommandHandler
    {
        private readonly CarProjectDbContext _context;
        public RemoveAboutCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }
        public async Task Handle(RemoveAboutCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.AboutId <= 0)
                    throw new ArgumentException("Invalid About ID provided", nameof(commands.AboutId));

                var values = await _context.Abouts.FindAsync(commands.AboutId);
                
                if (values == null)
                    throw new KeyNotFoundException($"About record with ID {commands.AboutId} not found");

                _context.Abouts.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the about record", ex);
            }
        }
    }
}
