using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.TestimonialCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers
{
    public class RemoveTestimonialCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public RemoveTestimonialCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveTestimonialCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Remove command cannot be null");

                if (commands.TestimonialId <= 0)
                    throw new ArgumentException("Invalid Testimonial ID provided", nameof(commands.TestimonialId));

                var values = await _context.Testimonials.FindAsync(commands.TestimonialId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Testimonial record with ID {commands.TestimonialId} not found");

                _context.Testimonials.Remove(values);
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
                throw new InvalidOperationException("An error occurred while removing the testimonial record", ex);
            }
        }
    }
}

