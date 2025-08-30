using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Commands.DistrictCommands
{
    public class RemoveDistrictCommands
    {
        public int DistrictId { get; set; }

        public RemoveDistrictCommands(int districtId)
        {
            DistrictId = districtId;
        }
    }
}
