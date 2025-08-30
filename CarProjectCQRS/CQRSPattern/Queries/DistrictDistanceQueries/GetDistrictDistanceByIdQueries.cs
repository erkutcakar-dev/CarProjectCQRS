namespace CarProjectCQRS.CQRSPattern.Queries.DistrictDistanceQueries
{
    public class GetDistrictDistanceByIdQueries
    {
        public int Id { get; set; }
        public GetDistrictDistanceByIdQueries(int id)
        {
            Id = id;
        }
    }
}
