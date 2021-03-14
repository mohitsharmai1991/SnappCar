using System;
using System.Collections.Generic;

namespace SnappCar.BLL
{
    /// <summary>
    /// RentPrice class entity
    /// </summary>
    public class RentPrice
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Dictionary<DateTime, double> PricePerDay { get; set; }

        public double Discount { get; set; }

        public double Insurance { get; set; }
        public double SnappCarCharges { get; set; }
        public double TotalRentPrice { get; set; }
        public string Message { get; set; }
    }
}
