using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SnappCar.BLL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnappCar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentCarController : ControllerBase
    {
        private readonly ILogger<RentCarController> _logger;

        private readonly ICarRental _carRental;

        public RentCarController(ILogger<RentCarController> logger, ICarRental carRental)
        {
            _logger = logger;
            _carRental = carRental;
        }

        /// <summary>
        /// Get all car details
        /// </summary>
        /// <returns>Car Details</returns>
        [HttpGet("GetAllCars")]
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await Task.Run(() => _carRental.GetAllCars());
        }

        /// <summary>
        /// Get all booking details
        /// </summary>
        /// <returns>Booking Details</returns>
        [HttpGet("GetAllBookings")]
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await Task.Run(() => _carRental.GetAllBookings());
        }

        /// <summary>
        /// Calculates the price on the basis of carId, fromDate and toDate
        /// </summary>
        /// <param name="carId">Unique Id for Car</param>
        /// <param name="fromDate">Bookin Start Date</param>
        /// <param name="toDate">Booking End Date</param>
        /// <returns>Rent Price</returns>
        [HttpGet("GetCarRentalPrice")]
        public async Task<RentPrice> GetCarRentalPrice(int carId, DateTime fromDate, DateTime toDate)
        {
            return await Task.Run(() => _carRental.CalculateCarRental(carId, fromDate, toDate));
        }
    }
}
