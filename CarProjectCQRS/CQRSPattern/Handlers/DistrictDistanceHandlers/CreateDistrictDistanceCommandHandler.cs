using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.DistrictDistanceCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.DistrictDistanceHandlers
{
    public class CreateDistrictDistanceCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateDistrictDistanceCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateDistrictDistanceCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "DistrictDistance command cannot be null");

                                _context.DistrictDistances.Add(new DistrictDistance()
                {
                  
                    Distance = commands.Distance,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the district distance record", ex);
            }
        }
    }
}
