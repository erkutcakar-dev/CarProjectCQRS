namespace CarProjectCQRS.CQRSPattern.Queries.FeatureQueries
{
    public class GetFeatureByIdQuery
    {
        public int FeatureId { get; set; }
        
        public GetFeatureByIdQuery()
        {
        }
        
        public GetFeatureByIdQuery(int featureId)
        {
            FeatureId = featureId;
        }
    }
}
