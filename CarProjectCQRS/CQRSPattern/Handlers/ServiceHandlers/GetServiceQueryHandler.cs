using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Service;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers
{
    public class GetServiceQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetServiceQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetServiceQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Services.ToListAsync();
                
                if (values == null)
                    return new List<GetServiceQueryResult>();

                return values.Select(x => new GetServiceQueryResult
                {
                    ServiceId = x.ServiceId,
                    Title = x.Title,
                    Description = x.Description,
                    Icon = x.Icon,
                    IconTitle = x.IconTitle,
                    IconSubtitle = x.IconSubtitle,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving service records", ex);
            }
        }
    }
}
