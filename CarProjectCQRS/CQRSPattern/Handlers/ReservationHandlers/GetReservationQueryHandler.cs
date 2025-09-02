using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Results.Reservation;
using Microsoft.EntityFrameworkCore;

namespace CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers
{
    public class GetReservationQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetReservationQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetReservationQueryResult>> Handle()
        {
            try
            {
                var values = await _context.Reservations.ToListAsync();
                
                if (values == null)
                    return new List<GetReservationQueryResult>();

                return values.Select(x => new GetReservationQueryResult
                {
                    ReservationId = x.ReservationId,
                    CarId = x.CarId,
                    CarType = x.CarType,
                    PickUpLocation = x.PickUpLocation,
                    DropOffLocation = x.DropOffLocation,
                    airport = x.airport,
                    PickUpDate = x.PickUpDate,
                    DropOffDate = x.DropOffDate,
                    IsActive = x.IsActive,
                    CreatedDate = x.CreatedDate,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving reservation records", ex);
            }
        }
    }
}


