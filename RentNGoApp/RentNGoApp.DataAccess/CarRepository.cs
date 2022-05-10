using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataAccess
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RentNGoAppContext rentNGoAppContext) : base(rentNGoAppContext)
        {
        }

        public List<Car> GetByFilter(Filter filter)
        {
            var result = _rentNGoAppContext.cars.AsQueryable().Where(car => car.status == Car.STATUS_AVAILABLE);
            if (filter.brand != 0)
            {
                result = result.Where(item => item.brand == filter.brand);
            }
            if (filter.color != 0)
            {
                result = result.Where(item => item.color == filter.color);
            }
            if (filter.minPrice != 0)
            {
                result = result.Where(item => item.price >= filter.minPrice);
            }
            if (filter.maxPrice != 0)
            {
                result = result.Where(item => item.price <= filter.maxPrice);
            }
            if (filter.sort != 0)
            {
                if (filter.sort == Sort.Ascending)
                {
                    result = result.OrderBy(item => item.price);
                }
                else
                {
                    result = result.OrderByDescending(item => item.price);
                }
            }
            return result.ToList();
        }
    }
}
