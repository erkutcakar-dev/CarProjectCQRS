using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Testimonial;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetTestimonialQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetTestimonialQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Testimonials.ToListAsync();
                
                if (values == null)
                    return new List<GetTestimonialQueryResult>();

                return values.Select(x => new GetTestimonialQueryResult
                {
                    TestimonialId = x.TestimonialId,
                    TestimonialName = x.TestimonialName,
                    Comment = x.Comment,
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving testimonial records", ex);
            }
        }
    }
}

