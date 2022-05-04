using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.AppLogic
{
    public class CarService : ICarService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CarService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<Car> GetAllCars()
        {
            var cars = new List<Car>();

            cars = _repositoryWrapper.carRepository.FindAll().ToList();

            return cars;
        }

        public void AddCar(Car car)
        {
            /*
            User user = new User();
            user.firstname = "John";
            user.lastname = "Doe";
            user.email = "john.doe@ymail.com";
            user.password = "12345678";
            //
            user.cars=new List<Car>();  
            user.cars.Add(car);

           /_repositoryWrapper.userRepository.Create(user);*/
           
            _repositoryWrapper.carRepository.Create(car);
            _repositoryWrapper.Save();
        }

        public Car GetCarById(int id)
        {
            Car car = _repositoryWrapper.carRepository.FindByCondition(item => item.carId == id).FirstOrDefault();
            return car;
        }

        public void Delete(int id)
        {
            Car car = GetCarById(id);
            _repositoryWrapper.carRepository.Delete(car);
            _repositoryWrapper.Save();
        }
    }
}
