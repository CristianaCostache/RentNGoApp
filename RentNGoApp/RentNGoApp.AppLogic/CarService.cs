using Microsoft.AspNetCore.Http;
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
        private IImageService _imageService;

        public CarService(IRepositoryWrapper repositoryWrapper, IImageService imageService)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
        }

        public List<Car> GetAllCars()
        {
            var cars = new List<Car>();

            cars = _repositoryWrapper.carRepository.FindAll().ToList();
            foreach (Car car in cars)
            {
                List<Image> images = _imageService.GetImagesByCarId(car.carId);
                car.images = images;
            }

            return cars;
        }

        public void AddCar(Car car, ICollection<IFormFile> imageFiles)
        {
            //User user = new User();
            //user.firstname = "John";
            //user.lastname = "Doe";
            //user.email = "john.doe@ymail.com";
            //user.password = "12345678";
            ////
            //user.cars=new List<Car>();  
            //user.cars.Add(car);

            //_repositoryWrapper.userRepository.Create(user);

            //_repositoryWrapper.carRepository.Create(car);

            ICollection<Image> images = _imageService.AddImage(imageFiles);
            car.images = images;
            User user = _repositoryWrapper.userRepository.FindByCondition(item => item.userId == 2).FirstOrDefault();
            user.cars = new List<Car>();
            user.cars.Add(car);

            _repositoryWrapper.userRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public List<Car> GetCarsByFilter(Filter filter)
        {
            List<Car> cars = _repositoryWrapper.carRepository.GetByFilter(filter);
            foreach (Car car in cars)
            {
                List<Image> images = _imageService.GetImagesByCarId(car.carId);
                car.images = images;
            }

            return cars;
        }

        public Car GetCarById(int id)
        {
            Car car = _repositoryWrapper.carRepository.FindByCondition(item => item.carId == id).FirstOrDefault();
            List<Image> images = _imageService.GetImagesByCarId(car.carId);
            car.images = images;
            return car;
        }

        public void Delete(int id)
        {
            Car car = GetCarById(id);
            _repositoryWrapper.carRepository.Delete(car);
            _repositoryWrapper.Save();
        }

        public List<Car> GetCarsByUserId(int userId)
        {
            List<Car> cars = _repositoryWrapper.carRepository.FindByCondition(car => car.userId == userId).ToList();
            return cars;
        }
    }
}
