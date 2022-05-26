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

        public List<Car> GetAllAvailableCars()
        {
            var cars = new List<Car>();

            cars = _repositoryWrapper.carRepository.FindByCondition(car => car.status == Car.STATUS_AVAILABLE).ToList();
            foreach (Car car in cars)
            {
                List<Image> images = _imageService.GetImagesByCarId(car.carId);
                car.images = images;
            }

            return cars;
        }

        public void AddCar(Car car, ICollection<IFormFile> imageFiles)
        {
            ICollection<Image> images = _imageService.AddImage(imageFiles);
            car.images = images;

            _repositoryWrapper.carRepository.Create(car);
            _repositoryWrapper.Save();
        }

        public List<Car> GetAvailableCarsByFilter(Filter filter)
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

        public List<Car> GetCarsByUserId(string userGuid)
        {
            List<Car> cars = _repositoryWrapper.carRepository.FindByCondition(car => car.userGuid == userGuid).ToList();
            return cars;
        }

        public List<Car> GetAllCars()
        {
            List<Car> cars = _repositoryWrapper.carRepository.FindAll().ToList();
            return cars;
        }

        public List<Car> GetAllUnavailableCars()
        {
            List<Car> cars = _repositoryWrapper.carRepository.FindByCondition(car => car.status == Car.STATUS_UNAVAILABLE).ToList();
            return cars;
        }
    }
}
