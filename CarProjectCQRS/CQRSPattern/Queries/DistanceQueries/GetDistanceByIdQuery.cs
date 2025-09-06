namespace CarProjectCQRS.CQRSPattern.Queries.DistanceQueries
{
    public class GetDistanceByIdQuery
    {
        public short DistanceId { get; set; }
        
        public GetDistanceByIdQuery()
        {
        }
        
        public GetDistanceByIdQuery(int distanceId)
        {
            DistanceId = (short)distanceId;
        }
    }
}
