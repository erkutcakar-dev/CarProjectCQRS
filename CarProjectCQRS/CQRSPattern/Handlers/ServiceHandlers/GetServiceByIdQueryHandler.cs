using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.ServiceQueries;
using CarProjectCQRS.CQRSPattern.Results.Service;

namespace CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetServiceByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetServiceByIdQueryResult> Handle(GetServiceByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Services.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetServiceByIdQueryResult
                {
                    ServiceId = values.ServiceId,
                    Title = values.Title,
                    Description = values.Description,
                    Icon = values.Icon,
                    IconTitle = values.IconTitle,
                    IconSubtitle = values.IconSubtitle,
                    IsActive = values.IsActive,
                    CreatedDate = values.CreatedDate,
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
                throw new InvalidOperationException("An error occurred while retrieving the service record", ex);
            }
        }
    }
}
