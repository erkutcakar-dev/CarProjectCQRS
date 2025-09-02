using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.FeatureCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers
{
    public class RemoveFeatureCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveFeatureCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveFeatureCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.FeatureId <= 0)
                    throw new ArgumentException("Invalid Feature ID provided", nameof(commands.FeatureId));

                var values = await _context.Features.FindAsync(commands.FeatureId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Feature record with ID {commands.FeatureId} not found");

                _context.Features.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the feature record", ex);
            }
        }
    }
}


