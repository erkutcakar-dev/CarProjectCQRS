using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ServiceCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers
{
    public class UpdateServiceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateServiceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateServiceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.ServiceId <= 0)
                    throw new ArgumentException("Invalid Service ID provided", nameof(commands.ServiceId));

                var values = await _context.Services.FindAsync(commands.ServiceId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Service record with ID {commands.ServiceId} not found");

                values.Title = commands.Title;
                values.Description = commands.Description;
                values.Icon = commands.Icon;
                values.IconTitle = commands.IconTitle;
                values.IconSubtitle = commands.IconSubtitle;
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
                throw new InvalidOperationException("An error occurred while updating the service record", ex);
            }
        }
    }
}

