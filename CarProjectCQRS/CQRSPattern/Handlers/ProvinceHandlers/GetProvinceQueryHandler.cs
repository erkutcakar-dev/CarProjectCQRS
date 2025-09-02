using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Province;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.ProvinceHandlers
{
    public class GetProvinceQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetProvinceQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetProvinceQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Provinces.ToListAsync();
                
                if (values == null)
                    return new List<GetProvinceQueryResult>();

                return values.Select(x => new GetProvinceQueryResult
                {
                    ProvinceId = x.ProvinceId,
                    ProvinceName = x.ProvinceName,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving province records", ex);
            }
        }
    }
}


