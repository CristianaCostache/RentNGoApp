using Microsoft.AspNetCore.Identity;
using RentNGoApp.Abstractions.Repositories;
using RentNGoApp.Abstractions.Services;
using RentNGoApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentNGoApp.AppLogic
{
    public class RentingInfoService : IRentingInfoService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private UserManager<IdentityUser> _userManager;
        private ICarService _carService;

        public RentingInfoService(IRepositoryWrapper repositoryWrapper, UserManager<IdentityUser> userManager, ICarService carService)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
            _carService = carService;
        }

		public List<RentingInfo> GetAllRentingInfos()
		{
			List<RentingInfo> rentingInfos = _repositoryWrapper.rentingInfoRepository.FindAll().ToList();
            return rentingInfos;
		}

		public RentingInfo GetOngoingRentingInfoByCarId(int id)
        {
            RentingInfo rentingInfo = _repositoryWrapper.rentingInfoRepository.FindByCondition(rentingInfo => rentingInfo.carId == id && rentingInfo.status == RentingInfo.STATUS_ONGOING).FirstOrDefault();
            return rentingInfo;
        }

        public List<RentingInfo> GetOngoingRentingInfosByUser(ClaimsPrincipal user)
        {
            string userGuid = _userManager.GetUserId(user);
            List<RentingInfo> rentingInfos = _repositoryWrapper.rentingInfoRepository.FindByCondition(rentingInfo => rentingInfo.userGuid == userGuid && rentingInfo.status == RentingInfo.STATUS_ONGOING).ToList();
            foreach (RentingInfo rentingInfo in rentingInfos)
            {
                Car car = _carService.GetCarById(rentingInfo.carId);
                rentingInfo.car = car;
                rentingInfo.userGuid = userGuid;
            }
            return rentingInfos;
        }

        public void Rent(int id, ClaimsPrincipal user)
        {
            string userGuid = _userManager.GetUserId(user);
            Car car = _carService.GetCarById(id);
            RentingInfo rentingInfo = new RentingInfo();
            rentingInfo.car = car;
            rentingInfo.userGuid = userGuid;

            car.status = Car.STATUS_UNAVAILABLE;

            _repositoryWrapper.carRepository.Update(car);
            _repositoryWrapper.rentingInfoRepository.Create(rentingInfo);

            _repositoryWrapper.Save();
        }

        public void UnrentCar(int id)
        {
            Car car = _carService.GetCarById(id);
            car.status = Car.STATUS_AVAILABLE;

            RentingInfo rentingInfo = GetOngoingRentingInfoByCarId(id);
            rentingInfo.status = RentingInfo.STATUS_EXPIRED;

            _repositoryWrapper.carRepository.Update(car);
            _repositoryWrapper.rentingInfoRepository.Update(rentingInfo);
            _repositoryWrapper.Save();
        }
    }
}
