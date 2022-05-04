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
    }
}
