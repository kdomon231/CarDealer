using CarDealerAPI.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public interface ICarService
    {
        Task<Car> AddCarAsync(Car car);
        Task<ICollection <Car>> GetAllCarsAsync();
        Task<CarRequest> UpdateCarAsync(CarRequest carRequest, string vin);
        Task<bool> DeleteCarAsync(string vin);
        Task<Car> GetCarByVINAsync(string vin);
    }
}
