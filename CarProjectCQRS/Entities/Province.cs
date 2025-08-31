namespace CarProjectCQRS.Entities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        // Navigation properties
        public virtual ICollection<District> Districts { get; set; } = new List<District>();
    }
}


