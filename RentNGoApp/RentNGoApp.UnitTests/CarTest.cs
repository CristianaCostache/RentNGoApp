using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.AppLogic;
using RentNGoApp.DataAccess;
using RentNGoApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace RentNGoApp.UnitTests
{
    [TestClass]
    public class CarTest
    {
        private const string userGuid = "9cdbaede-30f7-4b86-8eeb-14eb7677a973";
        private IRepositoryWrapper _repositoryWrapper;
        private UserManager<IdentityUser> _userManager;
        private ImageService _imageService;
        private CarService _carService;
        private RentingInfoService _rentingInfoService;

        [TestInitialize]
        public void Initialize()
        {
            DbContextOptionsBuilder<RentNGoAppContext> optionsBuilder = new DbContextOptionsBuilder<RentNGoAppContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RentNGoTestDb");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.EnableSensitiveDataLogging();

            RentNGoAppContext rentNGoContext = new RentNGoAppContext(optionsBuilder.Options);
            _repositoryWrapper = new RepositoryWrapper(rentNGoContext);

            _imageService = new ImageService(_repositoryWrapper, null);
            _carService = new CarService(_repositoryWrapper, _imageService);
            _rentingInfoService = new RentingInfoService(_repositoryWrapper, _userManager, _carService);
        }

        [TestMethod]
        public void AddCar_Test_ShouldLetTheUserAddCar()
        {
            Car car = new Car();
            car.name = "New car for renting";
            car.brand = Brand.MercedesBenz;
            car.color = Color.White;
            car.price = 50;
            car.description = "Best car";
            car.status = Car.STATUS_AVAILABLE;
            car.userGuid = userGuid;
            ICollection<IFormFile> imageFiles = new List<IFormFile>();

            _carService.AddCar(car, imageFiles);

            Car dbCar = _carService.GetAllAvailableCars().Last();

            Assert.AreEqual(car.name, dbCar.name);
        }

        [TestMethod]
        public void DeleteCar_Test_ShouldLetTheUserDeleteTheCar()
        {
            List<Car> dbAvailableCars = _carService.GetAllAvailableCars();
            int noAvailableCars = dbAvailableCars.Count();

            Car dbCar = dbAvailableCars.Last();
            _carService.Delete(dbCar.carId);

            List<Car> dbAvailableCarsAfter = _carService.GetAllAvailableCars();
            int noAvailableCarsAfter = dbAvailableCarsAfter.Count();

            Assert.AreEqual(noAvailableCars - noAvailableCarsAfter, 1);
        }

        [TestMethod]
        public void GetCarById_Test_ShouldLetTheUserGetCarById()
        {
            int carId = 33;

            Car car = _carService.GetCarById(carId);

            Assert.AreEqual(car.carId, carId);
        }

        [TestMethod]
        public void GetCarsByUserId_Test_ShouldLetTheUserGetCarsByUserId()
        {
            List<Car> cars = _carService.GetCarsByUserId(userGuid);

            foreach (Car car in cars)
            {
                Assert.AreEqual(car.userGuid, userGuid);
            }
        }
    }
}
