using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Commands.DistrictCommands
{
    public class CreateDistrictCommands
    {
      
        public string DistrictName { get; set; }

        // Navigation properties
        public virtual Province Province { get; set; }
        public virtual ICollection<DistrictDistance> DistrictDistanceFromDistricts { get; set; } = new List<DistrictDistance>();
        public virtual ICollection<DistrictDistance> DistrictDistanceToDistricts { get; set; } = new List<DistrictDistance>();
    }
}
