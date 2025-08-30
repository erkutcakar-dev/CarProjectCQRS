using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Commands.DistrictDistanceCommands
{
    public class CreateDistrictDistanceCommands
    {
     
        public decimal? Distance { get; set; }

        // Navigation properties
        public virtual District FromDistrict { get; set; }
        public virtual District ToDistrict { get; set; }
    }
}
