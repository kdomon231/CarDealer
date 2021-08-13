using CarDealerAPI.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public interface ICarService
    {
        Task<Car> AddCarAsync(CarRequest carRequest);
        Task<ICollection <Car>> GetAllCarsAsync();
        Task<CarRequest> UpdateCarAsync(CarRequest carRequest);
        Task<bool> DeleteCarAsync(string vin);
        Task<Car> GetCarByVINAsync(string vin);
    }
}
