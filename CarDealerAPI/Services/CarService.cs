using CarDealerAPI.Contracts.Requests;
using CarDealerAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class CarService : ICarService
    {
        public CarService(DataContext dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public DataContext Dbcontext { get; }

        public async Task<Car> AddCarAsync(Car car)
        {
            Car carAdded = new Car();         

            carAdded.VIN = car.VIN;
            carAdded.Brand = car.Brand;
            carAdded.Model = car.Model;
            carAdded.Year = car.Year;
            carAdded.Price = car.Price;

            Dbcontext.CarList.Add(carAdded);
            await Dbcontext.SaveChangesAsync();
            return carAdded;
        }

        public async Task<ICollection <Car>> GetAllCarsAsync()
        {
            var result = await Dbcontext.CarList.ToListAsync();
            return result;
        }
        public async Task<bool> DeleteCarAsync(string vin)
        {
            var item = await Dbcontext.CarList.SingleOrDefaultAsync(x => x.VIN == vin);
            if (item == null)
            {
                return false;
            }
            Dbcontext.CarList.Remove(item);
            await Dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<CarRequest> UpdateCarAsync(CarRequest carRequest, string vin)
        {
            Car updatedCar = await GetCarByVINAsync(vin);

            updatedCar.VIN = vin;
            updatedCar.Brand = carRequest.Brand;
            updatedCar.Model = carRequest.Model;
            updatedCar.Year = carRequest.Year;
            updatedCar.Price = carRequest.Price;

            Dbcontext.CarList.Update(updatedCar);
            await Dbcontext.SaveChangesAsync();
            return carRequest;
        }

        public async Task<Car> GetCarByVINAsync(string vin)
        {
            return await Dbcontext.CarList.SingleOrDefaultAsync(x => x.VIN == vin);
        }    
    }
}
