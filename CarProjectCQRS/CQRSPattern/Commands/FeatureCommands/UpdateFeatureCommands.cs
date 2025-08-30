namespace CarProjectCQRS.CQRSPattern.Commands.FeatureCommands
{
    public class UpdateFeatureCommands
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconTitle { get; set; }
        public string IconSubtitle { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
