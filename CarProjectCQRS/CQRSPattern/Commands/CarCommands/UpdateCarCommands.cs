namespace CarProjectCQRS.CQRSPattern.Commands.CarCommands
{
    public class UpdateCarCommands
    {
        public int CarId { get; set; }

        // Temel bilgiler
        public string Brand { get; set; }         // Hyundai
        public string Model { get; set; }         // Kona Electric
        public string ImageUrl { get; set; }      // Araç görseli

        // Değerlendirme
        public double ReviewScore { get; set; }   // 4.8        

        // Fiyatlandırma
        public decimal DailyPrice { get; set; }   // 187.00       

        // Teknik özellikler
        public int SeatCount { get; set; }        // 4
        public string TransmissionType { get; set; } // otomatik
        public string FuelType { get; set; }      // Petrol
        public int ModelYear { get; set; }        // 2015
        public string GearType { get; set; }      // AUTO
        public int Mileage { get; set; }          // 27000 km


        public bool IsAvailable { get; set; }     // Kiralamaya uygun mu?
        public DateTime CreatedDate { get; set; }
    }
}
