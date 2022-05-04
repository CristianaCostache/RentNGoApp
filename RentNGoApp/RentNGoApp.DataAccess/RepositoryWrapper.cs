﻿using RentNGoApp.Abstractions.Repositories;
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
        private IUserRepository? _userRepository;
        private IRentingInfoRepository? _rentingInfoRepository;

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

        public IUserRepository userRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_rentNGoAppContext);
                }

                return _userRepository;
            }
        }

        public IRentingInfoRepository rentingInfoRepository
        {
            get
            {
                if (_rentingInfoRepository == null)
                {
                    _rentingInfoRepository = new RentingInfoRepository(_rentNGoAppContext);
                }

                return _rentingInfoRepository;
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
