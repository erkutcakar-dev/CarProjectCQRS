using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.DistrictDistance;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictDistanceHandlers
{
    public class GetDistrictDistanceQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetDistrictDistanceQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetDistrictDistanceQueryResult>> Handle()
        {
            try
            {
                var values = await _context.DistrictDistances.ToListAsync();
                
                if (values == null)
                    return new List<GetDistrictDistanceQueryResult>();

                return values.Select(x => new GetDistrictDistanceQueryResult
                {
                    FromDistrictId = x.FromDistrictId,
                    ToDistrictId = x.ToDistrictId,
                    Distance = x.Distance,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving district distance records", ex);
            }
        }
    }
}
