using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.SliderCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers
{
    public class CreateSliderCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateSliderCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateSliderCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Slider command cannot be null");

                _context.Sliders.Add(new Slider()
                {
                    Title = commands.Title,
                    SubTitle = commands.SubTitle,
                    ImageUrl = commands.ImageUrl,
                    RedirectUrl = commands.RedirectUrl,
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
                throw new InvalidOperationException("An error occurred while creating the slider record", ex);
            }
        }
    }
}
