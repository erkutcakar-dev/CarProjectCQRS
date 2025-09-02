namespace CarProjectCQRS.Entities
{
    public class DistrictDistance
    {
        public int FromDistrictId { get; set; }
        public int ToDistrictId { get; set; }
        public decimal? Distance { get; set; }

        // Navigation properties
        public virtual District FromDistrict { get; set; }
        public virtual District ToDistrict { get; set; }
    }
}



