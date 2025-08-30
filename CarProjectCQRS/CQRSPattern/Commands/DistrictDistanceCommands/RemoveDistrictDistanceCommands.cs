using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Commands.DistrictDistanceCommands
{
    public class RemoveDistrictDistanceCommands
    {
        public int FromDistrictId { get; set; }

        public RemoveDistrictDistanceCommands(int fromDistrictId)
        {
            FromDistrictId = fromDistrictId;
        }
    }
}
