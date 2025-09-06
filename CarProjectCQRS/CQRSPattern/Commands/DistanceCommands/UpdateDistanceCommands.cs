namespace CarProjectCQRS.CQRSPattern.Commands.DistanceCommands
{
    public class UpdateDistanceCommands
    {
        public short DistanceId { get; set; }
        public string From { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public short DistanceValue { get; set; }
    }
}
