namespace CarProjectCQRS.CQRSPattern.Commands.DistanceCommands
{
    public class CreateDistanceCommands
    {
        public string From { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public short DistanceValue { get; set; }
    }
}
