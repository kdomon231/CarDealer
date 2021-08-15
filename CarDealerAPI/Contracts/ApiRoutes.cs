using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerAPI.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api/v1/";

        public static class Car
        {
            public const string AddCar = Base + "Car/Add";
            public const string GetAllCars = Base + "Car/GetAll";
            public const string GetCarByVIN = Base + "Car/GetCar/{vin}";
            public const string UpdateCar = Base + "Car/Update/{vin}";
            public const string DeleteCar = Base + "Car/Delete/{vin}";
        }
    }
}
