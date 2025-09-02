using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.TestimonialCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers
{
    public class CreateTestimonialCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateTestimonialCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateTestimonialCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Testimonial command cannot be null");

                _context.Testimonials.Add(new Testimonial()
                {
                    TestimonialName = commands.TestimonialName,
                    Comment = commands.Comment,
                    Rating = commands.Rating,
                    ImageUrl = commands.ImageUrl,
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
                throw new InvalidOperationException("An error occurred while creating the testimonial record", ex);
            }
        }
    }
}


