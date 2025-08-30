using CarProjectCQRS.Entities;

namespace CarProjectCQRS.CQRSPattern.Results.District
{
    public class GetDistrictByIdQueryResult
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; }

        // Navigation properties
       
    }
}
