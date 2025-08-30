namespace CarProjectCQRS.CQRSPattern.Results.DistrictDistance
{
    public class GetDistrictDistanceQueryResult
    {

        public int FromDistrictId { get; set; }
        public string FromDistrictName { get; set; }
        public int ToDistrictId { get; set; }
        public string ToDistrictName { get; set; }
        public decimal? Distance { get; set; }
    }
}
