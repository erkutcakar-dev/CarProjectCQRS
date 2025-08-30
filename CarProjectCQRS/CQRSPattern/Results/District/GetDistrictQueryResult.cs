using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Results.District
{
    public class GetDistrictQueryResult
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; }

        // Navigation properties
     


    }
}
