using CarProjectCQRS.CQRSPattern.Results.Car;
using CarProjectCQRS.CQRSPattern.Results.TurkeyAirport;
using CarProjectCQRS.CQRSPattern.Results.Distance;

namespace CarProjectCQRS.Models
{
    public class CarouselViewModel
    {
        public List<GetCarQueryResult> Cars { get; set; } = new List<GetCarQueryResult>();
        public List<GetTurkeyAirportQueryResult> Airports { get; set; } = new List<GetTurkeyAirportQueryResult>();
        public List<GetDistanceQueryResult> Distances { get; set; } = new List<GetDistanceQueryResult>();
    }
}
