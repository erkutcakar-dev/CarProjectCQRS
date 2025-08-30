using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.CarCommands;
using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Handlers.CarHandlers
{
    public class CreateCarCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public CreateCarCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCarCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Car command cannot be null");

                _context.Cars.Add(new Car()
                {
                    Brand = commands.Brand,
                    Model = commands.Model,
                    ImageUrl = commands.ImageUrl,
                    ReviewScore = commands.ReviewScore,
                    DailyPrice = commands.DailyPrice,
                    SeatCount = commands.SeatCount,
                    TransmissionType = commands.TransmissionType,
                    FuelType = commands.FuelType,
                    ModelYear = commands.ModelYear,
                    GearType = commands.GearType,
                    Mileage = commands.Mileage,
                    IsAvailable = commands.IsAvailable,
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
                throw new InvalidOperationException("An error occurred while creating the car record", ex);
            }
        }
    }
}
