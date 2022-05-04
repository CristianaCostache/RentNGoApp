using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        void AddCar(Car car);
        List<Car> GetCarsByFilter(Filter filter);
    }
}
