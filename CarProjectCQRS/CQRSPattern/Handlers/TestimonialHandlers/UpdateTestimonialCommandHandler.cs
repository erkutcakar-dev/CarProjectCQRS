using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.TestimonialCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers
{
    public class UpdateTestimonialCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateTestimonialCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTestimonialCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.TestimonialId <= 0)
                    throw new ArgumentException("Invalid Testimonial ID provided", nameof(commands.TestimonialId));

                var values = await _context.Testimonials.FindAsync(commands.TestimonialId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Testimonial record with ID {commands.TestimonialId} not found");

                values.TestimonialName = commands.TestimonialName;
                values.Comment = commands.Comment;
                values.Rating = commands.Rating;
                values.ImageUrl = commands.ImageUrl;
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
                throw new InvalidOperationException("An error occurred while updating the testimonial record", ex);
            }
        }
    }
}
