using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.EmployeeQueries;
using CarProjectCQRS.CQRSPattern.Results.Employee;

namespace CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers
{
    public class GetEmployeeByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetEmployeeByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeeByIdQueryResult> Handle(GetEmployeeByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Employees.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetEmployeeByIdQueryResult
                {
                    EmployeeId = values.EmployeeId,
                    FullName = values.FullName,
                    Position = values.Position,
                    Email = values.Email,
                    ImageUrl = values.ImageUrl,
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
                throw new InvalidOperationException("An error occurred while retrieving the employee record", ex);
            }
        }
    }
}

