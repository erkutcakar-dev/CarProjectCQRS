using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.EmployeeCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.EmployeeHandlers
{
    public class UpdateEmployeeCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateEmployeeCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateEmployeeCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.EmployeeId <= 0)
                    throw new ArgumentException("Invalid Employee ID provided", nameof(commands.EmployeeId));

                var values = await _context.Employees.FindAsync(commands.EmployeeId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Employee record with ID {commands.EmployeeId} not found");

                values.FullName = commands.FullName;
                values.Position = commands.Position;
                values.Email = commands.Email;
                values.ImageUrl = commands.ImageUrl;
                values.IsActive = commands.IsActive;
                values.CreatedDate = commands.CreatedDate;

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the employee record", ex);
            }
        }
    }
}

