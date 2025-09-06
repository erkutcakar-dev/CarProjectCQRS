namespace CarProjectCQRS.CQRSPattern.Commands.DistanceCommands
{
    public class RemoveDistanceCommands
    {
        public short DistanceId { get; set; }
        
        public RemoveDistanceCommands()
        {
        }
        
        public RemoveDistanceCommands(int distanceId)
        {
            DistanceId = (short)distanceId;
        }
    }
}
