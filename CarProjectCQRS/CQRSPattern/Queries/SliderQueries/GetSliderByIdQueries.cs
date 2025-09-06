namespace CarProjectCQRS.CQRSPattern.Queries.SliderQueries
{
    public class GetSliderByIdQueries
    {
        public int SliderId { get; set; }
        
        public GetSliderByIdQueries()
        {
        }
        
        public GetSliderByIdQueries(int sliderId)
        {
            SliderId = sliderId;
        }
    }
}
