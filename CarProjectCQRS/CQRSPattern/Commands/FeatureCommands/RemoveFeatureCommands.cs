 namespace CarProjectCQRS.CQRSPattern.Commands.FeatureCommands
{
    public class RemoveFeatureCommands
    {
        public int FeatureId { get; set; }

        public RemoveFeatureCommands(int featureId)
        {
            FeatureId = featureId;
        }
    }
}
