using System;
using System.Collections.Generic;

namespace CarProjectCQRS.Models
{
    public class EiaGasPriceResponse
    {
        public EiaDataResponse response { get; set; }
    }

    public class EiaDataResponse
    {
        public List<EiaDataPoint> data { get; set; }
    }

    public class EiaDataPoint
    {
        public string period { get; set; }
        public decimal value { get; set; }
    }
}
