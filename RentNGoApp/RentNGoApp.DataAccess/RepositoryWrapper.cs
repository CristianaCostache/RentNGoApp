using RentNGoApp.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.DataAccess
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RentNGoAppContext _rentNGoAppContext;
        private ICarRepository? _carRepository;

        public ICarRepository carRepository
        {
            get
            {
                if (_carRepository == null)
                {
                    _carRepository = new CarRepository(_rentNGoAppContext);
                }

                return _carRepository;
            }
        }

        public RepositoryWrapper(RentNGoAppContext rentNGoAppContext)
        {
            _rentNGoAppContext = rentNGoAppContext;
        }

        public void Save()
        {
            _rentNGoAppContext.SaveChanges();
        }
    }
}
