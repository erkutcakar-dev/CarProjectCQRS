namespace CarProjectCQRS.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        // Araç bilgisi
        public int CarId { get; set; }             // Seçilen aracın ID’si
        public string CarType { get; set; }               // Navigation property

        // Konum bilgileri
        public string PickUpLocation { get; set; } // Şehir veya Havalimanı
        public string DropOffLocation { get; set; } // Farklı teslim noktası
        public string airport { get; set; }

        // Tarih & saat ile şehir bilgisi bilgileri
        public DateTime PickUpDate { get; set; }      
        public DateTime DropOffDate { get; set; }       

        public bool IsActive { get; set; }         

        // Ortak alanlar
        public DateTime CreatedDate { get; set; }
    }

}
