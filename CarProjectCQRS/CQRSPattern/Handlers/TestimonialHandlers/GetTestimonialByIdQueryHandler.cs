using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.TestimonialQueries;
using CarProjectCQRS.CQRSPattern.Results.Testimonial;

namespace CarProjectCQRS.CQRSPattern.Handlers.TestimonialHandlers
{
    public class GetTestimonialByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetTestimonialByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetTestimonialByIdQueryResult> Handle(GetTestimonialByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Testimonials.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetTestimonialByIdQueryResult
                {
                    TestimonialId = values.TestimonialId,
                    TestimonialName = values.TestimonialName,
                    Comment = values.Comment,
                    Rating = values.Rating,
                    ImageUrl = values.ImageUrl,
                    IsActive = values.IsActive,
                    CreatedDate = values.CreatedDate,
                };
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the testimonial record", ex);
            }
        }
    }
}

