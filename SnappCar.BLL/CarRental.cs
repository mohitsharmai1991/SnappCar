using System;
using System.Collections.Generic;
using System.Linq;

namespace SnappCar.BLL
{
    /// <summary>
    /// Car rental class for all business logic
    /// </summary>
    public class CarRental : ICarRental
    {

        /// <summary>
        /// This method calculates the price on the basis of carId,fromDate and toDate
        /// </summary>
        /// <param name="carId">Unique Id for Car</param>
        /// <param name="fromDate">Booking start date</param>
        /// <param name="toDate">Booking end date</param>
        /// <returns>An object of RentPrice which consists of breakdown of price</returns>
        public RentPrice CalculateCarRental(int carId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                //// Fetching all cars and filtering by carId
                var cardetails = GetAllCars().Where(c => c.CarId == carId).FirstOrDefault();
                if (cardetails == null)
                {
                    return new RentPrice { Message = "Invalid request" };
                }
                RentPrice rentPrice = new RentPrice() { CarId = cardetails.CarId, CarName = cardetails.CarName, FromDate = fromDate, ToDate = toDate };

                //// Validations - if fromDate is less than today's date, if fromDate is greater than toDate, if car is already booked
                if (fromDate <= DateTime.Now || toDate < fromDate || IsBooked(carId, fromDate, toDate))
                {
                    rentPrice.Message = "Car is not available for the days selected or dates are invalid.";
                    return rentPrice;
                }

                Dictionary<DateTime, double> pricePerDay = new Dictionary<DateTime, double>();
                double totalBasePrice = 0;

                //// Fetching configurable percent object
                var percent = GetPercent();

                //// fetching and setting per day price in the RentPrice object
                for (var day = fromDate; day <= toDate; day = day.AddDays(1))
                {
                    var priceForDay = cardetails.PricePerDay;
                    if (IsWeekEnd(day.DayOfWeek))
                    {
                        priceForDay = priceForDay + CalculatePercent(cardetails.PricePerDay, percent.WeekendChargePercent);
                    }
                    totalBasePrice = totalBasePrice + priceForDay;
                    pricePerDay.Add(day, priceForDay);
                }

                rentPrice.PricePerDay = pricePerDay;

                var insurance = CalculatePercent(totalBasePrice, percent.InsurancePercent);
                var snappCarCharges = CalculatePercent(totalBasePrice, percent.InsurancePercent);

                double discount = 0;
                var totalPrice = totalBasePrice + insurance + snappCarCharges;

                //// Calculating discount if aplicable
                if (pricePerDay.Count > percent.DayCountForLongerBookingDiscount)
                {
                    discount = CalculatePercent(totalPrice, percent.DiscountPercent);
                    totalPrice = totalPrice - discount;
                }

                rentPrice.Insurance = insurance;
                rentPrice.SnappCarCharges = snappCarCharges;
                rentPrice.Discount = discount;
                rentPrice.TotalRentPrice = totalPrice;
                rentPrice.Message = "Book Now";

                return rentPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to fetch all cars
        /// </summary>
        /// <returns>car object</returns>
        public IEnumerable<Car> GetAllCars()
        {
            return new List<Car>
            {
                new Car{ CarId=1,CarName="Porsche",PricePerDay=100},
                new Car{ CarId=2,CarName="Tesla",PricePerDay=200},
                new Car{ CarId=3,CarName="Audi",PricePerDay=150},
                new Car{ CarId=4,CarName="Volvo",PricePerDay=120},
                new Car{ CarId=5,CarName="Bentley",PricePerDay=210},
                new Car{ CarId=6,CarName="Peugeot",PricePerDay=160}
            };
        }

        /// <summary>
        /// Method to fetch all bookings
        /// </summary>
        /// <returns>Booking object</returns>
        public IEnumerable<Booking> GetAllBookings()
        {
            return new List<Booking>
            {
                new Booking{ CarId=2,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(2)},
                new Booking{ CarId=4,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(4)},
                new Booking{ CarId=6,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(6)},
            };
        }

        /// <summary>
        /// Method to check if car is already booked
        /// </summary>
        /// <param name="carId">Unique Id for car</param>
        /// <param name="fromDate">Booking start date</param>
        /// <param name="toDate">Booking end date</param>
        /// <returns>boolean value</returns>
        public bool IsBooked(int carId, DateTime fromDate, DateTime toDate)
        {
            var bookings = GetAllBookings();
            for (var day = fromDate; day <= toDate; day = day.AddDays(1))
            {
                var checkBookingForCar = bookings.Where(c => c.CarId == carId && c.ToDate > day && c.FromDate < day).ToList().Count;

                if (checkBookingForCar > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method to calculate percentage of particular amount
        /// </summary>
        /// <param name="amount">Amount for percent calculation</param>
        /// <param name="percent">percentage param</param>
        /// <returns>double value</returns>
        public double CalculatePercent(double amount, double percent)
        {
            return amount * (percent / 100);
        }

        /// <summary>
        /// Method to check if it is a weekend
        /// </summary>
        /// <param name="day">Day of the week</param>
        /// <returns>boolean value</returns>
        public bool IsWeekEnd(DayOfWeek day)
        {
            if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to get configurable percentages
        /// </summary>
        /// <returns>Percent Object</returns>
        public Percent GetPercent()
        {
            return new Percent
            {
                DayCountForLongerBookingDiscount = 3,
                DiscountPercent = 15,
                InsurancePercent = 10,
                SnappCarChargePercent = 10,
                WeekendChargePercent = 5
            };
        }

    }
}
