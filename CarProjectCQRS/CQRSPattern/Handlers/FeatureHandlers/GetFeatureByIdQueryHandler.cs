using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.FeatureQueries;
using CarProjectCQRS.CQRSPattern.Results.Feature;

namespace CarProjectCQRS.CQRSPattern.Handlers.FeatureHandlers
{
    public class GetFeatureByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetFeatureByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetFeatureByIdQueryResult> Handle(GetEmployeeByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Features.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetFeatureByIdQueryResult
                {
                    FeatureId = values.FeatureId,
                    Title = values.Title,
                    Description = values.Description,
                    Icon = values.Icon,
                    IconTitle = values.IconTitle,
                    IconSubtitle = values.IconSubtitle,
                    ImageUrl = values.ImageUrl,
                    IsActive = values.IsActive,
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
                throw new InvalidOperationException("An error occurred while retrieving the feature record", ex);
            }
        }
    }
}

