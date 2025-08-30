using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.CarQueries;
using CarProjectCQRS.CQRSPattern.Results.Car;

namespace CarProjectCQRS.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetCarByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetCarByIdQueryResult> Handle(GetCarByIdQuery query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.Id <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.Id));

                var values = await _context.Cars.FindAsync(query.Id);

                if (values == null)
                    return null;

                return new GetCarByIdQueryResult
                {
                    CarId = values.CarId,
                    Brand = values.Brand,
                    Model = values.Model,
                    ImageUrl = values.ImageUrl,
                    ReviewScore = values.ReviewScore,
                    DailyPrice = values.DailyPrice,
                    SeatCount = values.SeatCount,
                    TransmissionType = values.TransmissionType,
                    FuelType = values.FuelType,
                    ModelYear = values.ModelYear,
                    GearType = values.GearType,
                    Mileage = values.Mileage,
                    IsAvailable = values.IsAvailable,
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
                throw new InvalidOperationException("An error occurred while retrieving the car record", ex);
            }
        }
    }
}
