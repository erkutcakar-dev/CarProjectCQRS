namespace CarProjectCQRS.CQRSPattern.Commands.ProvinceCommands
{
    public class RemoveProvinceCommands
    {
        public int ProvinceId { get; set; }

        public RemoveProvinceCommands(int provinceId)
        {
            ProvinceId = provinceId;
        }
    }
}
