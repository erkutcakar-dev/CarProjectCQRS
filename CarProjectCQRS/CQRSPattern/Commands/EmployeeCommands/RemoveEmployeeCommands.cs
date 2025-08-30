namespace CarProjectCQRS.CQRSPattern.Commands.EmployeeCommands
{
    public class RemoveEmployeeCommands
    {
        public int EmployeeId { get; set; }

        public RemoveEmployeeCommands(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
