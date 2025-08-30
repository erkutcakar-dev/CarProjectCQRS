namespace CarProjectCQRS.CQRSPattern.Results.Employee
{
    public class GetEmployeeByIdQueryResult
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
