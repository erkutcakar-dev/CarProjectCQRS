using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.DistanceQueries;
using CarProjectCQRS.CQRSPattern.Results.Distance;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers
{
    public class GetDistanceQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetDistanceQueryHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<GetDistanceQueryResult>> Handle()
        {
            try
            {
                var distances = await _context.Distances
                    .Select(d => new GetDistanceQueryResult
                    {
                        DistanceId = d.DistanceId,
                        From = d.From,
                        Destination = d.Destination,
                        DistanceValue = d.DistanceValue
                    })
                    .ToListAsync();

                Console.WriteLine($"GetDistanceQueryHandler: Found {distances?.Count ?? 0} distances");
                return distances ?? new List<GetDistanceQueryResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDistanceQueryHandler Error: {ex.Message}");
                throw new InvalidOperationException($"Error retrieving distances: {ex.Message}", ex);
            }
        }
    }
}
