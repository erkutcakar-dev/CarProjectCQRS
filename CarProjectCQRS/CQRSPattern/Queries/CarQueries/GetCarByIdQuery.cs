namespace CarProjectCQRS.CQRSPattern.Queries.CarQueries
{
    public class GetCarByIdQuery
    {
        public int Id { get; set; }
        public GetCarByIdQuery(int id)
        {
            Id = id;
        }
    }
}
