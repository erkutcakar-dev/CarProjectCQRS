namespace CarProjectCQRS.CQRSPattern.Queries.TurkeyAirportQueries
{
    public class GetTurkeyAirportByIdQueries
    {
        public int Id { get; set; }
        public GetTurkeyAirportByIdQueries(int id)
        {
            Id = id;
        }
    }
}
