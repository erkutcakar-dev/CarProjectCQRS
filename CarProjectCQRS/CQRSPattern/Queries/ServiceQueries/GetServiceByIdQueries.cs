namespace CarProjectCQRS.CQRSPattern.Queries.ServiceQueries
{
    public class GetServiceByIdQueries
    {
        public int Id { get; set; }

        public GetServiceByIdQueries(int id)
        {
            Id = id;
        }
    }
}
