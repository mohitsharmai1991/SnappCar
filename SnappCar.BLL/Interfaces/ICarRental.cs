using System;
using System.Collections.Generic;

namespace SnappCar.BLL
{
    /// <summary>
    /// CarRental Interface
    /// </summary>
    public interface ICarRental
    {
        /// <summary>
        /// This method calculates the price on the basis of carId,fromDate and toDate
        /// </summary>
        /// <param name="carId">Unique Id for Car</param>
        /// <param name="fromDate">Booking start date</param>
        /// <param name="toDate">Booking end date</param>
        /// <returns>An object of RentPrice which consists of breakdown of price</returns>
        RentPrice CalculateCarRental(int carId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Method to fetch all cars
        /// </summary>
        /// <returns>car object</returns>
        IEnumerable<Car> GetAllCars();

        /// <summary>
        /// Method to fetch all bookings
        /// </summary>
        /// <returns>Booking object</returns>
        IEnumerable<Booking> GetAllBookings();

        /// <summary>
        /// Method to check if car is already booked
        /// </summary>
        /// <param name="carId">Unique Id for car</param>
        /// <param name="fromDate">Booking start date</param>
        /// <param name="toDate">Booking end date</param>
        /// <returns>boolean value</returns>
        bool IsBooked(int carId, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Method to calculate percentage of particular amount
        /// </summary>
        /// <param name="amount">Amount for percent calculation</param>
        /// <param name="percent">percentage param</param>
        /// <returns>double value</returns>
        double CalculatePercent(double amount, double percent);

        /// <summary>
        /// Method to check if it is a weekend
        /// </summary>
        /// <param name="day">Day of the week</param>
        /// <returns>boolean value</returns>
        bool IsWeekEnd(DayOfWeek day);

        /// <summary>
        /// Method to get configurable percentages
        /// </summary>
        /// <returns>Percent Object</returns>
        Percent GetPercent();
    }
}
