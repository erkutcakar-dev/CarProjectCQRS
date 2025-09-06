using CarProjectCQRS.Entities;

namespace CarProjectCQRS.Models
{
    public class CarBookingSearchModel
    {
        public int CarId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string PickUpLocation { get; set; } = string.Empty;
        public string DropOffLocation { get; set; } = string.Empty;
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public int? AirportId { get; set; }
        public string AirportName { get; set; } = string.Empty;
        public int Distance { get; set; }
    }

    public class AvailableCarsViewModel
    {
        public List<Car> AvailableCars { get; set; } = new List<Car>();
        public CarBookingSearchModel SearchCriteria { get; set; } = new CarBookingSearchModel();
        public int TotalDays { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

