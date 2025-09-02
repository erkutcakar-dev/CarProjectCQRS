using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Employee;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetEmployeeQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetEmployeeQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Employees.ToListAsync();
                
                if (values == null)
                    return new List<GetEmployeeQueryResult>();

                return values.Select(x => new GetEmployeeQueryResult
                {
                    EmployeeId = x.EmployeeId,
                    FullName = x.FullName,
                    Position = x.Position,
                    Email = x.Email,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving employee records", ex);
            }
        }
    }
}


