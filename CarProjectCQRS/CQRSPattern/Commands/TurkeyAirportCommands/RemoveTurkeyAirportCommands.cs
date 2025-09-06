namespace CarProjectCQRS.CQRSPattern.Commands.TurkeyAirportCommands
{
    public class RemoveTurkeyAirportCommands
    {
        public byte AirPortId { get; set; }
        
        public RemoveTurkeyAirportCommands()
        {
        }
        
        public RemoveTurkeyAirportCommands(int airPortId)
        {
            AirPortId = (byte)airPortId;
        }
    }
}
