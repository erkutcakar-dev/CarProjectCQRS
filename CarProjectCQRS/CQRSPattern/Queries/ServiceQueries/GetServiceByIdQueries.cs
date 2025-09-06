namespace CarProjectCQRS.CQRSPattern.Queries.ServiceQueries
{
    public class GetServiceByIdQueries
    {
        public int ServiceId { get; set; }
        
        public GetServiceByIdQueries()
        {
        }
        
        public GetServiceByIdQueries(int serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
