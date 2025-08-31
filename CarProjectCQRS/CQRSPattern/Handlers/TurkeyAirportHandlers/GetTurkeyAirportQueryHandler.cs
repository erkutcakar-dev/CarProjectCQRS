using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.TurkeyAirport;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers
{
    public class GetTurkeyAirportQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetTurkeyAirportQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetTurkeyAirportQueryResult>> Handle()
        {
            try
            {
                var values = await _context.TurkeyAirports.ToListAsync();
                
                if (values == null)
                    return new List<GetTurkeyAirportQueryResult>();

                return values.Select(x => new GetTurkeyAirportQueryResult
                {
                    AirPortId = x.AirPortId,
                    Province = x.Province,
                    AirportName = x.AirportName,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving turkey airport records", ex);
            }
        }
    }
}

