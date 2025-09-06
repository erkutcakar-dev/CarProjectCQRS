namespace CarProjectCQRS.CQRSPattern.Queries.TurkeyAirportQueries
{
    public class GetTurkeyAirportByIdQueries
    {
        public byte AirPortId { get; set; }
        
        public GetTurkeyAirportByIdQueries()
        {
        }
        
        public GetTurkeyAirportByIdQueries(int airPortId)
        {
            AirPortId = (byte)airPortId;
        }
    }
}
