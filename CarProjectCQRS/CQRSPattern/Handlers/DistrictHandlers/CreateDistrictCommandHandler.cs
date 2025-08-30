using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistrictCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictHandlers
{
    public class CreateDistrictCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateDistrictCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateDistrictCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "District command cannot be null");

                                _context.Districts.Add(new District()
                {
                  
                    DistrictName = commands.DistrictName,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the district record", ex);
            }
        }
    }
}
