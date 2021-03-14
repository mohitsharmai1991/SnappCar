namespace SnappCar.BLL
{
    /// <summary>
    /// Percent class entity
    /// </summary>
    public class Percent
    {
        public double DiscountPercent { get; set; }
        public double DayCountForLongerBookingDiscount { get; set; }

        public double InsurancePercent { get; set; }
        public double SnappCarChargePercent { get; set; }

        public double WeekendChargePercent { get; set; }
    }
}
