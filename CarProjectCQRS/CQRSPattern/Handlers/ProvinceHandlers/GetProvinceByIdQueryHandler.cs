using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.ProvinceQueries;
using CarProjectCQRS.CQRSPattern.Results.Province;

namespace CarProjectCQRS.CQRSPattern.Handlers.ProvinceHandlers
{
    public class GetProvinceByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetProvinceByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetProvinceByIdQueryResult> Handle(GetProvinceByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Provinces.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetProvinceByIdQueryResult
                {
                    ProvinceId = values.ProvinceId,
                    ProvinceName = values.ProvinceName,
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
                throw new InvalidOperationException("An error occurred while retrieving the province record", ex);
            }
        }
    }
}


