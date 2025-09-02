using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.ServiceCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.ServiceHandlers
{
    public class CreateServiceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateServiceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateServiceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Service command cannot be null");

                _context.Services.Add(new Service()
                {
                    Title = commands.Title,
                    Description = commands.Description,
                    Icon = commands.Icon,
                    IconTitle = commands.IconTitle,
                    IconSubtitle = commands.IconSubtitle,
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
                throw new InvalidOperationException("An error occurred while creating the service record", ex);
            }
        }
    }
}


