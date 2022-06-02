using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.Abstractions.Repositories
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        List<Car> GetByFilter(Filter filter);
        List<Car> GetAllCars();
        List<Car> GetAllAvailableCars();
        Car GetCarById(int id);
    }
}
