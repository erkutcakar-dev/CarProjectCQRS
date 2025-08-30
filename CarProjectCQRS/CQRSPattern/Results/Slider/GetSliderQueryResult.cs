namespace CarProjectCQRS.CQRSPattern.Results.Slider
{
    public class GetSliderQueryResult
    {
        public int SliderId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
