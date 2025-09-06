namespace CarProjectCQRS.CQRSPattern.Commands.TestimonialCommands
{
    public class RemoveTestimonialCommands
    {
        public int TestimonialId { get; set; }
        
        public RemoveTestimonialCommands()
        {
        }
        
        public RemoveTestimonialCommands(int testimonialId)
        {
            TestimonialId = testimonialId;
        }
    }
}
