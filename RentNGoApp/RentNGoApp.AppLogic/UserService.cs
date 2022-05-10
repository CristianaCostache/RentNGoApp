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
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private ICarService _carService;
        private IRentingInfoService _rentingInfoService;

        public UserService(IRepositoryWrapper repositoryWrapper, ICarService carService, IRentingInfoService rentingInfoService)
        {
            _repositoryWrapper = repositoryWrapper;
            _carService = carService;
            _rentingInfoService = rentingInfoService;
        }
        public void AddUser(User user)
        {
            _repositoryWrapper.userRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public ProfileViewModel GetUserData()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            
            User user = _repositoryWrapper.userRepository.FindByCondition(user => user.userId == 2).FirstOrDefault(); // todo
            profileViewModel.user = user;
            
            List<Car> cars = _carService.GetCarsByUserId(user.userId);
            profileViewModel.cars = cars;

            List<RentingInfo> rentingInfos = _rentingInfoService.GetOngoingRentingInfosByUserId(user.userId);
            profileViewModel.rentingInfos = rentingInfos;

            return profileViewModel;
        }
    }
}
