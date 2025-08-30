using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.AboutCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers
{
    public class UpdateAboutCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateAboutCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateAboutCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.AboutId <= 0)
                    throw new ArgumentException("Invalid About ID provided", nameof(commands.AboutId));

                var values = await _context.Abouts.FindAsync(commands.AboutId);
                
                if (values == null)
                    throw new KeyNotFoundException($"About record with ID {commands.AboutId} not found");

                values.Title = commands.Title;
                values.Description = commands.Description;
                values.VisionTitle = commands.VisionTitle;
                values.VisionDescription = commands.VisionDescription;
                values.MissionTitle = commands.MissionTitle;
                values.MissionDescription = commands.MissionDescription;
                values.YearsOfExperience = commands.YearsOfExperience;
                values.ExperienceItem1 = commands.ExperienceItem1;
                values.ExperienceItem2 = commands.ExperienceItem2;
                values.ExperienceItem3 = commands.ExperienceItem3;
                values.ExperienceItem4 = commands.ExperienceItem4;
                values.FounderName = commands.FounderName;
                values.FounderTitle = commands.FounderTitle;
                values.FounderImageUrl = commands.FounderImageUrl;
                values.MainImageUrl = commands.MainImageUrl;
                values.SecondaryImageUrl = commands.SecondaryImageUrl;
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
                throw new InvalidOperationException("An error occurred while updating the about record", ex);
            }
        }
    }
}
