using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.TurkeyAirportQueries;
using CarProjectCQRS.CQRSPattern.Results.TurkeyAirport;

namespace CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers
{
    public class GetTurkeyAirportByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetTurkeyAirportByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetTurkeyAirportByIdQueryResult> Handle(GetTurkeyAirportByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.AirPortId <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.AirPortId));

                var values = await _context.TurkeyAirports.FindAsync(query.AirPortId);

                if (values == null)
                    return null;

                return new GetTurkeyAirportByIdQueryResult
                {
                    AirPortId = values.AirPortId,
                    Province = values.Province,
                    AirportName = values.AirportName,
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
                throw new InvalidOperationException("An error occurred while retrieving the turkey airport record", ex);
            }
        }
    }
}

