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
            public const string AddCar = Base + "Car";
            public const string GetAllCars = Base + "Car";
            public const string GetCarByVIN = Base + "Car/{vin}";
            public const string UpdateCar = Base + "Car/{vin}";
            public const string DeleteCar = Base + "Car/{vin}";
        }
    }
}
