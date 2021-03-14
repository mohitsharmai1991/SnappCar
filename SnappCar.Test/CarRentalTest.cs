using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnappCar.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnappCar.Test
{
    /// <summary>
    /// Test cases for CarRental
    /// </summary>
    [TestClass]
    public class CarRentalTest
    {
        /// <summary>
        /// ICarRental
        /// </summary>
        private readonly ICarRental _carRental;

        /// <summary>
        /// Constructor for CarRentalTest
        /// </summary>
        public CarRentalTest()
        {
            _carRental = new CarRental();
        }

        /// <summary>
        /// Test case for Calculating Car rental with discount
        /// </summary>
        [TestMethod]
        public void TestCalculateCarRentalWithDiscount()
        {

            var actual = _carRental.CalculateCarRental(1, Convert.ToDateTime("29-03-2021"), Convert.ToDateTime("01-04-2021"));
            Assert.AreEqual(1, actual.CarId);
            Assert.AreEqual("Porsche", actual.CarName);
            Assert.AreEqual(72, actual.Discount);
            Assert.AreEqual(Convert.ToDateTime("29-03-2021"), actual.FromDate);
            Assert.AreEqual(Convert.ToDateTime("01-04-2021"), actual.ToDate);
            Assert.AreEqual(40, actual.Insurance);
            Assert.AreEqual(40, actual.SnappCarCharges);
            Assert.AreEqual(4, actual.PricePerDay.Count);
            Assert.AreEqual(408, actual.TotalRentPrice);
            Assert.AreEqual("Book Now", actual.Message);
        }

        /// <summary>
        /// Test case for Calculating Car rental without discount
        /// </summary>
        [TestMethod]
        public void TestCalculateCarRentalWithoutDiscount()
        {

            var actual = _carRental.CalculateCarRental(1, Convert.ToDateTime("29-03-2021"), Convert.ToDateTime("30-03-2021"));
            Assert.AreEqual(1, actual.CarId);
            Assert.AreEqual("Porsche", actual.CarName);
            Assert.AreEqual(0, actual.Discount);
            Assert.AreEqual(Convert.ToDateTime("29-03-2021"), actual.FromDate);
            Assert.AreEqual(Convert.ToDateTime("30-03-2021"), actual.ToDate);
            Assert.AreEqual(20, actual.Insurance);
            Assert.AreEqual(20, actual.SnappCarCharges);
            Assert.AreEqual(2, actual.PricePerDay.Count);
            Assert.AreEqual(240, actual.TotalRentPrice);
            Assert.AreEqual("Book Now", actual.Message);
        }

        /// <summary>
        /// Test case for Calculating Car rental for already booked car
        /// </summary>
        [TestMethod]
        public void TestCalculateCarRentalForAlreadyBookedCar()
        {

            var actual = _carRental.CalculateCarRental(2, DateTime.Now, DateTime.Now.AddDays(2));
            Assert.AreEqual(2, actual.CarId);
            Assert.AreEqual("Tesla", actual.CarName);
            Assert.AreEqual(0, actual.Discount);
            Assert.AreEqual(DateTime.Now.Date, actual.FromDate.Date);
            Assert.AreEqual(DateTime.Now.AddDays(2).Date, actual.ToDate.Date);
            Assert.AreEqual(0, actual.Insurance);
            Assert.AreEqual(0, actual.SnappCarCharges);
            Assert.AreEqual(null, actual.PricePerDay);
            Assert.AreEqual(0, actual.TotalRentPrice);
            Assert.AreEqual("Car is not available for the days selected or dates are invalid.", actual.Message);

        }

        /// <summary>
        /// Test case for Calculating Car where from date is today or before
        /// </summary>
        [TestMethod]
        public void TestCalculateCarRentalForDateBeforeorToday()
        {

            var actual = _carRental.CalculateCarRental(1, Convert.ToDateTime("13-03-2021"), Convert.ToDateTime("17-03-2021"));
            Assert.AreEqual(1, actual.CarId);
            Assert.AreEqual("Porsche", actual.CarName);
            Assert.AreEqual(0, actual.Discount);
            Assert.AreEqual(Convert.ToDateTime("13-03-2021").Date, actual.FromDate);
            Assert.AreEqual(Convert.ToDateTime("17-03-2021").Date, actual.ToDate);
            Assert.AreEqual(0, actual.Insurance);
            Assert.AreEqual(0, actual.SnappCarCharges);
            Assert.AreEqual(null, actual.PricePerDay);
            Assert.AreEqual(0, actual.TotalRentPrice);
            Assert.AreEqual("Car is not available for the days selected or dates are invalid.", actual.Message);
        }

        /// <summary>
        /// Test case for Calculating Car where from date is greater that to date
        /// </summary>
        [TestMethod]
        public void TestCalculateCarRentalForFromDateGreaterThanToDate()
        {

            var actual = _carRental.CalculateCarRental(1, Convert.ToDateTime("17-03-2021"), Convert.ToDateTime("14-03-2021"));
            Assert.AreEqual(1, actual.CarId);
            Assert.AreEqual("Porsche", actual.CarName);
            Assert.AreEqual(0, actual.Discount);
            Assert.AreEqual(Convert.ToDateTime("17-03-2021").Date, actual.FromDate);
            Assert.AreEqual(Convert.ToDateTime("14-03-2021").Date, actual.ToDate);
            Assert.AreEqual(0, actual.Insurance);
            Assert.AreEqual(0, actual.SnappCarCharges);
            Assert.AreEqual(null, actual.PricePerDay);
            Assert.AreEqual(0, actual.TotalRentPrice);
            Assert.AreEqual("Car is not available for the days selected or dates are invalid.", actual.Message);
        }

        /// <summary>
        /// Test case for fetching all cars
        /// </summary>
        [TestMethod]
        public void TestGetAllCars()
        {
            var expected = new List<Car>
            {
                new Car{ CarId=1,CarName="Porsche",PricePerDay=100},
                new Car{ CarId=2,CarName="Tesla",PricePerDay=200},
                new Car{ CarId=3,CarName="Audi",PricePerDay=150},
                new Car{ CarId=4,CarName="Volvo",PricePerDay=120},
                new Car{ CarId=5,CarName="Bentley",PricePerDay=210},
                new Car{ CarId=6,CarName="Peugeot",PricePerDay=160}
            };

            var actual = _carRental.GetAllCars().ToList();

            Assert.IsTrue(expected.Count == actual.Count, $"Lists have different sizes. Expected list: {expected.Count}, actual list: {actual.Count}");

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].CarId, actual[i].CarId);
                Assert.AreEqual(expected[i].CarName, actual[i].CarName);
                Assert.AreEqual(expected[i].PricePerDay, actual[i].PricePerDay);
            }

        }

        /// <summary>
        /// Test case for fetching all bookings
        /// </summary>
        [TestMethod]
        public void TestGetAllBookings()
        {
            var expected = new List<Booking>
            {
                new Booking{ CarId=2,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(2)},
                new Booking{ CarId=4,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(4)},
                new Booking{ CarId=6,FromDate=DateTime.Now,ToDate=DateTime.Now.AddDays(6)},
            };

            var actual = _carRental.GetAllBookings().ToList();
            Assert.IsTrue(expected.Count == actual.Count, $"Lists have different sizes. Expected list: {expected.Count}, actual list: {actual.Count}");

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].CarId, actual[i].CarId);
                Assert.AreEqual(expected[i].FromDate.Date, actual[i].FromDate.Date);
                Assert.AreEqual(expected[i].ToDate.Date, actual[i].ToDate.Date);
            }
        }

        /// <summary>
        ///  Test case to check if car is already booked
        /// </summary>
        [TestMethod]
        public void TestIsBooked()
        {
            var actual = _carRental.IsBooked(2, DateTime.Now, DateTime.Now.AddDays(1));
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///  Test case to check if car is not booked
        /// </summary>
        [TestMethod]
        public void TestIsNotBooked()
        {
            var actual = _carRental.IsBooked(1, DateTime.Now, DateTime.Now.AddDays(1));
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Test case to calculate percent
        /// </summary>
        [TestMethod]
        public void TestCalculatePercent()
        {
            var actual = _carRental.CalculatePercent(100, 10);
            Assert.AreEqual(actual, 10);
        }

        /// <summary>
        /// Test case to check if day is weekend
        /// </summary>
        [TestMethod]
        public void TestIsWeekEnd()
        {
            var actual = _carRental.IsWeekEnd(Convert.ToDateTime("14-03-2021").DayOfWeek);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Test case to check if day is not weekend
        /// </summary>
        [TestMethod]
        public void TestIsNotWeekEnd()
        {
            var actual = _carRental.IsWeekEnd(Convert.ToDateTime("15-03-2021").DayOfWeek);
            Assert.IsFalse(actual);
        }

        /// <summary>
        /// Test case to get percent
        /// </summary>
        [TestMethod]
        public void TestGetPercent()
        {
            var expected = new Percent
            {
                DayCountForLongerBookingDiscount = 3,
                DiscountPercent = 15,
                InsurancePercent = 10,
                SnappCarChargePercent = 10,
                WeekendChargePercent = 5
            };

            var actual = _carRental.GetPercent();
            Assert.AreEqual(expected.DayCountForLongerBookingDiscount, actual.DayCountForLongerBookingDiscount);
            Assert.AreEqual(expected.DiscountPercent, actual.DiscountPercent);
            Assert.AreEqual(expected.InsurancePercent, actual.InsurancePercent);
            Assert.AreEqual(expected.SnappCarChargePercent, actual.SnappCarChargePercent);
            Assert.AreEqual(expected.WeekendChargePercent, actual.WeekendChargePercent);
        }
    }
}
