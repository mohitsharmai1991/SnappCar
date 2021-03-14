using System;

namespace SnappCar.BLL
{
    /// <summary>
    /// Booking class entity
    /// </summary>
    public class Booking
    {
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
