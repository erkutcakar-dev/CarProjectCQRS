using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.TurkeyAirportCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.TurkeyAirportHandlers
{
    public class CreateTurkeyAirportCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateTurkeyAirportCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateTurkeyAirportCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "TurkeyAirport command cannot be null");

                                _context.TurkeyAirports.Add(new TurkeyAirport()
                {
                    Province = commands.Province,
                    AirportName = commands.AirportName,
                });

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the turkey airport record", ex);
            }
        }
    }
}
