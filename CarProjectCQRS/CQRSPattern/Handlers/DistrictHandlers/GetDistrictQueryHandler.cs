using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.District;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictHandlers
{
    public class GetDistrictQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetDistrictQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetDistrictQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Districts.ToListAsync();
                
                if (values == null)
                    return new List<GetDistrictQueryResult>();

                return values.Select(x => new GetDistrictQueryResult
                {
                    DistrictId = x.DistrictId,
                    ProvinceId = x.ProvinceId,
                    DistrictName = x.DistrictName,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving district records", ex);
            }
        }
    }
}
