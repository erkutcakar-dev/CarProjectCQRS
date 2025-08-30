namespace CarProjectCQRS.CQRSPattern.Queries.ReservationQueries
{
    public class GetReservationByIdQueries
    {
        public int Id { get; set; }
        public GetReservationByIdQueries(int id)
        {
            Id = id;
        }
    }
}
