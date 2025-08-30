namespace CarProjectCQRS.CQRSPattern.Queries.DistrictQueries
{
    public class GetDistrictByIdQueries
    {
        public int Id { get; set; }

        public GetDistrictByIdQueries(int id)
        {
            Id = id;
        }
    }
}
