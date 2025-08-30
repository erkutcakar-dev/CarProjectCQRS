namespace CarProjectCQRS.CQRSPattern.Queries.SliderQueries
{
    public class GetSliderByIdQueries
    {
        public int Id { get; set; }
        public GetSliderByIdQueries(int id)
        {
            Id = id;
        }
    }
}
