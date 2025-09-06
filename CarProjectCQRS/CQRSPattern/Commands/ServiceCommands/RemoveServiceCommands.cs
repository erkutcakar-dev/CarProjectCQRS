namespace CarProjectCQRS.CQRSPattern.Commands.ServiceCommands
{
    public class RemoveServiceCommands
    {
        public int ServiceId { get; set; }
        
        public RemoveServiceCommands()
        {
        }
        
        public RemoveServiceCommands(int serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
