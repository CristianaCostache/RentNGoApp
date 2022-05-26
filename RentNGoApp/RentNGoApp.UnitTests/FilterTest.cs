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
    public class FilterTest
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
        public void SortFilter_Test_ShouldLetTheUserFilterTheCarsUsingSort()
        {
            Filter filter = new Filter();
            filter.sort = Sort.Ascending;
            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            Car referenceCar = filterCars.First();
            for (int i = 1; i < filterCars.Count; i++)
            {
                Assert.IsTrue(filterCars[i].price >= referenceCar.price);
                referenceCar = filterCars[i];
            }


            filter.sort = Sort.Descending;
            filterCars = _carService.GetAvailableCarsByFilter(filter);

            referenceCar = filterCars.First();
            for (int i = 1; i < filterCars.Count; i++)
            {
                Assert.IsTrue(filterCars[i].price <= referenceCar.price);
                referenceCar = filterCars[i];
            }
        }

        [TestMethod]
        public void BrandFilter_Test_ShouldLetTheUserFilterTheCarsUsingBrand()
        {
            Filter filter = new Filter();
            filter.brand = Brand.Dacia;
            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            foreach (Car car in filterCars)
            {
                Assert.IsTrue(car.brand.Equals(filter.brand));
            }
        }

        [TestMethod]
        public void ColorFilter_Test_ShouldLetTheUserFilterTheCarsUsingColor()
        {
            Filter filter = new Filter();
            filter.color = Color.White;
            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            foreach (Car car in filterCars)
            {
                Assert.IsTrue(car.color.Equals(filter.color));
            }
        }

        [TestMethod]
        public void PriceFilter_Test_ShouldLetTheUserFilterTheCarsUsingPrice()
        {
            Filter filter = new Filter();
            filter.minPrice = 70;
            filter.maxPrice = 100;
            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            foreach (Car car in filterCars)
            {
                Assert.IsTrue(car.price >= filter.minPrice && car.price <= filter.maxPrice);
            }
        }

        [TestMethod]
        public void BrandColor_Test_ShouldLetTheUserFilterTheCarsUsingBrandColor()
        {
            Filter filter = new Filter();
            filter.brand = Brand.Dacia;
            filter.color = Color.White;

            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            foreach (Car car in filterCars)
            {
                Assert.AreEqual(car.brand, filter.brand);
                Assert.AreEqual(car.color, filter.color);
            }
        }

        [TestMethod]
        public void AllFilter_Test_ShouldLetTheUserFilterTheCarsUsingAllFilters()
        {
            Filter filter = new Filter();
            filter.brand = Brand.Dacia;
            filter.color = Color.White;
            filter.minPrice = 50;
            filter.maxPrice = 65;

            List<Car> filterCars = _carService.GetAvailableCarsByFilter(filter);

            foreach (Car car in filterCars)
            {
                Assert.AreEqual(car.brand, filter.brand);
                Assert.AreEqual(car.color, filter.color);
                Assert.IsTrue(car.price >= filter.minPrice && car.price <= filter.maxPrice);
            }
        }
    }
}
