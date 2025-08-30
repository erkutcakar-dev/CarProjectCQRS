namespace CarProjectCQRS.CQRSPattern.Queries.FeatureQueries
{
    public class GetEmployeeByIdQueries
    {
        public int Id { get; set; }

        public GetEmployeeByIdQueries(int id)
        {
            Id = id;
        }
    }
}
