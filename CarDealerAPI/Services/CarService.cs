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

        public async Task<Car> AddCarAsync(CarRequest carRequest)
        {

            Car carAdded = new Car();         

            carAdded.Id = new Guid();
            carAdded.VIN = carRequest.VIN;
            carAdded.Brand = carRequest.Brand;
            carAdded.Model = carRequest.Model;
            carAdded.Year = carRequest.Year;
            carAdded.Price = carRequest.Price;

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
        public async Task<CarRequest> UpdateCarAsync(CarRequest carRequest)
        {
            Car car = new Car();
            //car.Id = new Guid();
            car.VIN = carRequest.VIN;
            car.Brand = carRequest.Brand;
            car.Model = carRequest.Model;
            car.Year = carRequest.Year;
            car.Price = carRequest.Price;

            Dbcontext.CarList.Update(car);
            await Dbcontext.SaveChangesAsync();
            return carRequest;
        }

        public async Task<Car> GetCarByVINAsync(string vin)
        {
            return await Dbcontext.CarList.SingleOrDefaultAsync(x => x.VIN == vin);
        }    
    }
}
