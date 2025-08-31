using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.SliderCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers
{
    public class RemoveSliderCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveSliderCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveSliderCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.SliderId <= 0)
                    throw new ArgumentException("Invalid Slider ID provided", nameof(commands.SliderId));

                var values = await _context.Sliders.FindAsync(commands.SliderId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Slider record with ID {commands.SliderId} not found");

                _context.Sliders.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the slider record", ex);
            }
        }
    }
}

