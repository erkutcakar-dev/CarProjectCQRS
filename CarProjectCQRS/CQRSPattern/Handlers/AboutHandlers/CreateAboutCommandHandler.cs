using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.AboutCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.AboutHandlers
{
    public class CreateAboutCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateAboutCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateAboutCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "About command cannot be null");

                _context.Abouts.Add(new About()
                {
                    Title = commands.Title,
                    Description = commands.Description,
                    VisionTitle = commands.VisionTitle,
                    VisionDescription = commands.VisionDescription,
                    MissionTitle = commands.MissionTitle,
                    MissionDescription = commands.MissionDescription,
                    YearsOfExperience = commands.YearsOfExperience,
                    ExperienceItem1 = commands.ExperienceItem1,
                    ExperienceItem2 = commands.ExperienceItem2,
                    ExperienceItem3 = commands.ExperienceItem3,
                    ExperienceItem4 = commands.ExperienceItem4,
                    FounderName = commands.FounderName,
                    FounderTitle = commands.FounderTitle,
                    FounderImageUrl = commands.FounderImageUrl,
                    MainImageUrl = commands.MainImageUrl,
                    SecondaryImageUrl = commands.SecondaryImageUrl,
                    IsActive = commands.IsActive,
                    CreatedDate = DateTime.Now,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the about record", ex);
            }
        }
        }
}
