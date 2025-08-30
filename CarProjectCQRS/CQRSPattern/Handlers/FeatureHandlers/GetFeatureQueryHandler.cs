using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Feature;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetFeatureQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetFeatureQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Features.ToListAsync();
                
                if (values == null)
                    return new List<GetFeatureQueryResult>();

                return values.Select(x => new GetFeatureQueryResult
                {
                    FeatureId = x.FeatureId,
                    Title = x.Title,
                    Description = x.Description,
                    Icon = x.Icon,
                    IconTitle = x.IconTitle,
                    IconSubtitle = x.IconSubtitle,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving feature records", ex);
            }
        }
    }
}
