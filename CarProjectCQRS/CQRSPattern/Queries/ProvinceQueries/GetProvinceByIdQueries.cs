namespace CarProjectCQRS.CQRSPattern.Queries.ProvinceQueries
{
    public class GetProvinceByIdQueries
    {
        public int Id { get; set; }
        public GetProvinceByIdQueries(int id)
        {
            Id = id;
        }
    }
}
