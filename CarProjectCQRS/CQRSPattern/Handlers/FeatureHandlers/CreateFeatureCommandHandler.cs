using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.FeatureCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers
{
    public class CreateFeatureCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateFeatureCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateFeatureCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Feature command cannot be null");

                _context.Features.Add(new Feature()
                {
                    Title = commands.Title,
                    Description = commands.Description,
                    Icon = commands.Icon,
                    IconTitle = commands.IconTitle,
                    IconSubtitle = commands.IconSubtitle,
                    ImageUrl = commands.ImageUrl,
                    IsActive = commands.IsActive,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the feature record", ex);
            }
        }
    }
}

