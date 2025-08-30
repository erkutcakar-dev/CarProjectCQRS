using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Car;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.CarHandlers
{
    public class GetCarQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetCarQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetCarQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Cars.ToListAsync();
                
                if (values == null)
                    return new List<GetCarQueryResult>();

                return values.Select(x => new GetCarQueryResult
                {
                    CarId = x.CarId,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = x.ImageUrl,
                    ReviewScore = x.ReviewScore,
                    DailyPrice = x.DailyPrice,
                    SeatCount = x.SeatCount,
                    TransmissionType = x.TransmissionType,
                    FuelType = x.FuelType,
                    ModelYear = x.ModelYear,
                    GearType = x.GearType,
                    Mileage = x.Mileage,
                    IsAvailable = x.IsAvailable,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving car records", ex);
            }
        }
    }
}
