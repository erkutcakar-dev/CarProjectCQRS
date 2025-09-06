using CarProjectCQRS.Context;
using CarProjectCQRS.CQRSPattern.Queries.ReservationQueries;
using CarProjectCQRS.CQRSPattern.Results.Reservation;

namespace CarProjectCQRS.CQRSPattern.Handlers.ReservationHandlers
{
    public class GetReservationByIdQueryHandler
    {
        private readonly CarProjectDbContext _context;

        public GetReservationByIdQueryHandler(CarProjectDbContext context)
        {
            _context = context;
        }

        public async Task<GetReservationByIdQueryResult> Handle(GetReservationByIdQueries query)
        {
            try
            {
                if (query == null)
                    throw new ArgumentNullException(nameof(query), "Query cannot be null");

                if (query.ReservationId <= 0)
                    throw new ArgumentException("Invalid ID provided", nameof(query.ReservationId));

                var values = await _context.Reservations.FindAsync(query.ReservationId);

                if (values == null)
                    return null;

                return new GetReservationByIdQueryResult
                {
                    ReservationId = values.ReservationId,
                    CarId = values.CarId,
                    CarType = values.CarType,
                    PickUpLocation = values.PickUpLocation,
                    DropOffLocation = values.DropOffLocation,
                    airport = values.airport,
                    PickUpDate = values.PickUpDate,
                    DropOffDate = values.DropOffDate,
                    IsActive = values.IsActive,
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
                throw new InvalidOperationException("An error occurred while retrieving the reservation record", ex);
            }
        }
    }
}


