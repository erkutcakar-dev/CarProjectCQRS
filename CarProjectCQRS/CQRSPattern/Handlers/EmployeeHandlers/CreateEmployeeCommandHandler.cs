using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.EmployeeCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers
{
    public class CreateEmployeeCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateEmployeeCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEmployeeCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Employee command cannot be null");

                _context.Employees.Add(new Employee()
                {
                    FullName = commands.FullName,
                    Position = commands.Position,
                    Email = commands.Email,
                    ImageUrl = commands.ImageUrl,
                    IsActive = commands.IsActive,
                    CreatedDate = DateTime.Now,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the employee record", ex);
            }
        }
    }
}


