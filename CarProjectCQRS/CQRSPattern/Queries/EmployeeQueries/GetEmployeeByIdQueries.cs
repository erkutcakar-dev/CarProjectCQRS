namespace CarProjectCQRS.CQRSPattern.Queries.EmployeeQueries
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
