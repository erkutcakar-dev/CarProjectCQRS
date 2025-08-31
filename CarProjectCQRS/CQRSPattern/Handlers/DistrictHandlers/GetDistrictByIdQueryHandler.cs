using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.DistrictQueries;
using CarProjectCQRS.CQRSPattern.Results.District;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictHandlers
{
    public class GetDistrictByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetDistrictByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetDistrictByIdQueryResult> Handle(GetDistrictByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Districts.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetDistrictByIdQueryResult
                {
                    DistrictId = values.DistrictId,
                    ProvinceId = values.ProvinceId,
                    DistrictName = values.DistrictName,
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
                throw new InvalidOperationException("An error occurred while retrieving the district record", ex);
            }
        }
    }
}

