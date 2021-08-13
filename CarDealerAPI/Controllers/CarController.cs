using CarDealerAPI.Contracts;
using CarDealerAPI.Contracts.Requests;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{
    public class CarController : Controller
    {
        public CarController(ICarService carService)
        {
            CarService = carService;
        }

        public ICarService CarService { get; }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Car.AddCar)]
        public async Task<IActionResult> CarAdd([FromBody] CarRequest carRequest)
        {
            var result = await CarService.AddCarAsync(carRequest);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Car.GetAllCars)]
        public async Task<IActionResult> GetAllCarsAsync ()
        {
            var result = await CarService.GetAllCarsAsync();
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Car.UpdateCar)]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] string vin, [FromBody] CarRequest carRequest)
        {
            var updatedCar = await CarService.GetCarByVINAsync(vin);
            if (updatedCar == null)
            {
                return NotFound();
            }
            updatedCar.VIN = carRequest.VIN;
            updatedCar.Brand = carRequest.Brand;
            updatedCar.Model = carRequest.Model;
            updatedCar.Year = carRequest.Year;
            updatedCar.Price = carRequest.Price;
            var result = await CarService.UpdateCarAsync(carRequest);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpDelete(ApiRoutes.Car.DeleteCar)]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] string vin)
        {
            var delete = await CarService.DeleteCarAsync(vin);
            if (delete)
                return NoContent();
            return NotFound();
        }
    }
}
