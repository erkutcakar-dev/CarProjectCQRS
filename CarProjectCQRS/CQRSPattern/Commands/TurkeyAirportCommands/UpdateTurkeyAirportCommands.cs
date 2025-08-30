namespace CarProjectCQRS.CQRSPattern.Commands.TurkeyAirportCommands
{
    public class UpdateTurkeyAirportCommands
    {
        public byte AirPortId { get; set; }
        public string Province { get; set; }
        public string AirportName { get; set; }
    }
}
