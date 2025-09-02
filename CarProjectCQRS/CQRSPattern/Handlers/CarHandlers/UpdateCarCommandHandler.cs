using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Commands.CarCommands;

namespace CarProjectCQRS.CQRSPattern.Handlers.CarHandlers
{
    public class UpdateCarCommandHandler
    {
        private readonly CarProjectDbContext _context;

        public UpdateCarCommandHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCarCommands commands)
        {
            try
            {
                if (commands == null)
                    throw new ArgumentNullException(nameof(commands), "Update command cannot be null");

                if (commands.CarId <= 0)
                    throw new ArgumentException("Invalid Car ID provided", nameof(commands.CarId));

                var values = await _context.Cars.FindAsync(commands.CarId);
                
                if (values == null)
                    throw new KeyNotFoundException($"Car record with ID {commands.CarId} not found");

                values.Brand = commands.Brand;
                values.Model = commands.Model;
                values.ImageUrl = commands.ImageUrl;
                values.ReviewScore = commands.ReviewScore;
                values.DailyPrice = commands.DailyPrice;
                values.SeatCount = commands.SeatCount;
                values.TransmissionType = commands.TransmissionType;
                values.FuelType = commands.FuelType;
                values.ModelYear = commands.ModelYear;
                values.GearType = commands.GearType;
                values.Mileage = commands.Mileage;
                values.IsAvailable = commands.IsAvailable;
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
                throw new InvalidOperationException("An error occurred while updating the car record", ex);
            }
        }
    }
}


