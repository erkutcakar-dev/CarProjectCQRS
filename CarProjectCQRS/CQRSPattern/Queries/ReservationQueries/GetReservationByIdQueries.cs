namespace CarProjectCQRS.CQRSPattern.Queries.ReservationQueries
{
    public class GetReservationByIdQueries
    {
        public int ReservationId { get; set; }
        
        public GetReservationByIdQueries()
        {
        }
        
        public GetReservationByIdQueries(int reservationId)
        {
            ReservationId = reservationId;
        }
    }
}
