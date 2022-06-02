using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.AppLogic;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.UnitTests
{
    [TestClass]
    public class CarMockTest
    {
        private Mock<IRepositoryWrapper> _repositoryWrapperMock = new Mock<IRepositoryWrapper>();
        private Mock<IImageService> _imageService=new Mock<IImageService>();

        [TestInitialize]
        public void Initialize(){
            List<Car> carList = new List<Car>()
            {
                new Car(){ carId = 1, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 2, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_UNAVAILABLE},
                new Car(){ carId = 3, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_UNAVAILABLE},
                new Car(){ carId = 4, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 5, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 6, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 7, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_UNAVAILABLE},
                new Car(){ carId = 8, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 9, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_UNAVAILABLE},
            };


            List<Car> carAvailableList = new List<Car>()
            {
                new Car(){ carId = 1, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 2, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 3, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
                new Car(){ carId = 4, name = "Nume" , color=Color.Black, brand=Brand.Dacia, price=100, description="Merge", status=Car.STATUS_AVAILABLE},
            };

            _repositoryWrapperMock.Setup(carRepo => carRepo.carRepository.GetAllCars())
                        .Returns(carList);
            _repositoryWrapperMock.Setup(carRepo => carRepo.carRepository.GetAllAvailableCars())
                        .Returns(carAvailableList);
            _repositoryWrapperMock.Setup(carRepo => carRepo.carRepository.GetCarById(9))
                        .Returns(carList.Last());

        }

        [TestMethod]
        public void GetAllCars_ReturnsCarList()
        {
            var carService = new CarService(_repositoryWrapperMock.Object, _imageService.Object);
            var carList = new List<Car>();

            carList = carService.GetAllCars();
            Assert.AreEqual(9, carList.Count);
        }

        [TestMethod]
        public void GetAllAvailableCars_ReturnsCarAvailableList()
        {
            var carService = new CarService(_repositoryWrapperMock.Object, _imageService.Object);
            var carList = new List<Car>();
            carList = carService.GetAllAvailableCars();
            Assert.AreEqual(4, carList.Count);
        }

        [TestMethod]
        public void GetCarById_ReturnsCarWithId()
        {
            var carService = new CarService(_repositoryWrapperMock.Object, _imageService.Object);
            Car lastCar = carService.GetAllCars().Last();
            Car car;
            car = carService.GetCarById(lastCar.carId);
            Assert.AreEqual(lastCar.carId, car.carId);
        }
    }
}
