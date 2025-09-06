using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.DistanceQueries;
using CarProjectCQRS.CQRSPattern.Results.Distance;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistanceHandlers
{
    public class GetDistanceByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetDistanceByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetDistanceByIdQueryResult?> Handle(GetDistanceByIdQuery query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query));

                var distance = await _context.Distances
                    .Where(d => d.DistanceId == query.DistanceId)
                    .Select(d => new GetDistanceByIdQueryResult
                    {
                        DistanceId = d.DistanceId,
                        From = d.From,
                        Destination = d.Destination,
                        DistanceValue = d.DistanceValue,
                    })
                    .FirstOrDefaultAsync();

                if (distance == null)
                    throw new KeyNotFoundException($"Distance with ID {query.DistanceId} not found.");

                return distance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving distance by ID: {ex.Message}", ex);
            }
        }
    }
}
