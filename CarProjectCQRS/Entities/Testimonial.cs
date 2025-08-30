namespace CarProjectCQRS.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string TestimonialName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1-5 arası
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
