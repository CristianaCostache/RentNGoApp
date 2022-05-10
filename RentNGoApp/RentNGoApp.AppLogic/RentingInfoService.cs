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
    public class RentingInfoService : IRentingInfoService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private ICarService _carService;

        public RentingInfoService(IRepositoryWrapper repositoryWrapper, ICarService carService)
        {
            _repositoryWrapper = repositoryWrapper;
            _carService = carService;
        }

        public RentingInfo GetOngoingRentingInfoByCarId(int id)
        {
            RentingInfo rentingInfo = _repositoryWrapper.rentingInfoRepository.FindByCondition(rentingInfo => rentingInfo.carId == id && rentingInfo.status == RentingInfo.STATUS_ONGOING).FirstOrDefault();
            return rentingInfo;
        }

        public List<RentingInfo> GetOngoingRentingInfosByUserId(int userId)
        {
            List<RentingInfo> rentingInfos = _repositoryWrapper.rentingInfoRepository.FindByCondition(rentingInfo => rentingInfo.userId == userId && rentingInfo.status == RentingInfo.STATUS_ONGOING).ToList();
            foreach (RentingInfo rentingInfo in rentingInfos)
            {
                Car car = _carService.GetCarById(rentingInfo.carId);
                rentingInfo.car = car;
                rentingInfo.user = _repositoryWrapper.userRepository.FindByCondition(user => user.userId == car.userId).FirstOrDefault();
            }
            return rentingInfos;
        }

        public void Rent(int id)
        {
            Car car = _carService.GetCarById(id);
            User user = _repositoryWrapper.userRepository.FindByCondition(item => item.userId == 2).FirstOrDefault(); // todo
            RentingInfo rentingInfo = new RentingInfo();
            rentingInfo.car = car;
            rentingInfo.user = user;

            car.status = Car.STATUS_UNAVAILABLE;

            _repositoryWrapper.userRepository.Update(user);
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
