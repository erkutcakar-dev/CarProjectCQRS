namespace CarProjectCQRS.CQRSPattern.Queries.TestimonialQueries
{
    public class GetTestimonialByIdQueries
    {
        public int TestimonialId { get; set; }
        
        public GetTestimonialByIdQueries()
        {
        }
        
        public GetTestimonialByIdQueries(int testimonialId)
        {
            TestimonialId = testimonialId;
        }
    }
}
