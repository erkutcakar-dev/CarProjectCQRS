using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.SliderQueries;
using CarProjectCQRS.CQRSPattern.Results.Slider;

namespace CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetSliderByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetSliderByIdQueryResult> Handle(GetSliderByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Sliders.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetSliderByIdQueryResult
                {
                    SliderId = values.SliderId,
                    Title = values.Title,
                    SubTitle = values.SubTitle,
                    ImageUrl = values.ImageUrl,
                    RedirectUrl = values.RedirectUrl,
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
                throw new InvalidOperationException("An error occurred while retrieving the slider record", ex);
            }
        }
    }
}

