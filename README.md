# SnappCar API

There are three projects in this solution :-

1. SnappCar.API : This is the main API which consists of 3 endpoints :-
/api/RentCar/GetAllCars - to fetch all cars
/api/RentCar/GetAllBookings - to fetch all bookings
/api/RentCar/GetCarRentalPrice - to calculate price breakdown on the basis of carId which can be found via GetAllCars endpoint, fromDate and toDate.

Input dates should be in correct format (Eg. - 03-14-2021)

Swagger is also activated for the api project.

2. SnappCar.BLL : This is the Business logic layer where all the logic is done. This project is refrenced in SnappCar.API

Percentage provided for Discount,SnappCar fee percent is configurable via object.

3. SnappCar.Test :  This is the Test project for SnappCar. BLL layer is unit tested.

API project can be executed directly via visual studio code, or can be published in local IIS or can be hosted on cloud.




