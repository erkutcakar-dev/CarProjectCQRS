using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.SliderCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers
{
    public class UpdateSliderCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateSliderCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateSliderCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.SliderId <= 0)
                    throw new ArgumentException("Invalid Slider ID provided", nameof(commands.SliderId));

                var values = await _context.Sliders.FindAsync(commands.SliderId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Slider record with ID {commands.SliderId} not found");

                values.Title = commands.Title;
                values.SubTitle = commands.SubTitle;
                values.ImageUrl = commands.ImageUrl;
                values.RedirectUrl = commands.RedirectUrl;
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
                throw new InvalidOperationException("An error occurred while updating the slider record", ex);
            }
        }
    }
}


