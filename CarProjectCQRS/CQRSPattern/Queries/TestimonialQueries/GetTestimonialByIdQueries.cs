namespace CarProjectCQRS.CQRSPattern.Queries.TestimonialQueries
{
    public class GetTestimonialByIdQueries
    {
        public int Id { get; set; }
        public GetTestimonialByIdQueries(int id)
        {
            Id = id;
        }
    }
}
