using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.FeatureCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers
{
    public class UpdateFeatureCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateFeatureCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateFeatureCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.FeatureId <= 0)
                    throw new ArgumentException("Invalid Feature ID provided", nameof(commands.FeatureId));

                var values = await _context.Features.FindAsync(commands.FeatureId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Feature record with ID {commands.FeatureId} not found");

                values.Title = commands.Title;
                values.Description = commands.Description;
                values.Icon = commands.Icon;
                values.IconTitle = commands.IconTitle;
                values.IconSubtitle = commands.IconSubtitle;
                values.ImageUrl = commands.ImageUrl;
                values.IsActive = commands.IsActive;

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
                throw new InvalidOperationException("An error occurred while updating the feature record", ex);
            }
        }
    }
}
