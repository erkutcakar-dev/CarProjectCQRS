using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Slider;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.SliderHandlers
{
    public class GetSliderQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetSliderQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetSliderQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Sliders.ToListAsync();
                
                if (values == null)
                    return new List<GetSliderQueryResult>();

                return values.Select(x => new GetSliderQueryResult
                {
                    SliderId = x.SliderId,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    ImageUrl = x.ImageUrl,
                    RedirectUrl = x.RedirectUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving slider records", ex);
            }
        }
    }
}


