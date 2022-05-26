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
    public class RentingInfoTest
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
        public void RentCar_Test_ShouldLetTheUserRentTheCar()
        {
            List<Car> dbAvailableCars = _carService.GetAllAvailableCars();
            Car dbCar = dbAvailableCars.Last();

            _rentingInfoService.Rent(dbCar.carId, dbCar.userGuid);

            Car dbCarAfter = _carService.GetAllCars().Last();

            Assert.AreEqual(dbCarAfter.status, Car.STATUS_UNAVAILABLE);
        }

        [TestMethod]
        public void ReturnCar_Test_ShouldLetTheUserReturnTheCar()
        {
            List<Car> dbUnavailableCars = _carService.GetAllUnavailableCars();
            Car dbCar = dbUnavailableCars.Last();

            _rentingInfoService.UnrentCar(dbCar.carId);

            Car dbCarAfter = _carService.GetAllCars().Last();

            Assert.AreEqual(dbCarAfter.status, Car.STATUS_AVAILABLE);
        }

        [TestMethod]
        public void GetAllAvailableCars_Test_ShouldGetAllAvailableCars()
        {
            List<Car> dbAvailableCars = _carService.GetAllAvailableCars();
            int noAvailableCars = dbAvailableCars.Count();

            Car dbCar = dbAvailableCars.Last();

            _rentingInfoService.Rent(dbCar.carId, dbCar.userGuid);

            dbAvailableCars = _carService.GetAllAvailableCars();
            int noAvailableCarsAfter = dbAvailableCars.Count();

            Assert.AreEqual(noAvailableCars - noAvailableCarsAfter, 1);
        }

        [TestMethod]
        public void GetOngoingRentingInfoByCarId_Test_ShouldGetOngoingRentingInfoByCarId()
        {
            List<Car> dbAvailableCars = _carService.GetAllUnavailableCars();
            Car dbCar = dbAvailableCars.Last();

            RentingInfo rentingInfo = _rentingInfoService.GetOngoingRentingInfoByCarId(dbCar.carId);

            Assert.AreEqual(rentingInfo.status, RentingInfo.STATUS_ONGOING);
            Assert.AreEqual(rentingInfo.carId, dbCar.carId);
        }

        [TestMethod]
        public void GetOngoingRentingInfosByUser_Test_ShouldGetOngoingRentingInfosByUser()
        {
            List<RentingInfo> rentingInfos = _rentingInfoService.GetOngoingRentingInfosByUser(userGuid);

            foreach (RentingInfo rentingInfo in rentingInfos)
            {
                Assert.AreEqual(rentingInfo.status, RentingInfo.STATUS_ONGOING);
                Assert.AreEqual(rentingInfo.userGuid, userGuid);
            }
        }
    }
}